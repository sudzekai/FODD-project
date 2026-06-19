using DB.dbContext;
using DB.models;
using Microsoft.EntityFrameworkCore;
using Services.statuses.interfaces;
using Services.unitOfWork;
using Shared.dtos.statuses;
using Shared.types.exceptions;

namespace Services.statuses.services
{
    public class StatusesService : IStatusesService
    {
        private readonly DbSet<Status> _statuses;
        private readonly IUnitOfWork _uow;

        public StatusesService(ShoesStoreDbContext ctx, IUnitOfWork uow)
        {
            _statuses = ctx.Statuses;
            _uow = uow;
        }

        public async Task<List<StatusDTO>> GetStatusesAsync()
           => await _statuses.Select(s => new StatusDTO(s.Id, s.Name)).ToListAsync();

        public async Task<StatusDTO> GetStatusByIdAsync(int id)
        {
            var entry = await _statuses.FirstOrDefaultAsync(s => s.Id == id) ??
                throw new NotFoundException("Статус с таким id не найден");

            return new(entry.Id, entry.Name);
        }

        public async Task<int> CreateStatusAsync(StatusWriteDTO dto)
        {
            if (await IsNameExistsAsync(dto.Name))
                throw new ConflictException("Статус с таким названием уже существует");

            var created = await _statuses.AddAsync(new() { Name = dto.Name });

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new InternalServerException("Возникла ошибка при создании статуса");
            }

            return created.Entity.Id;
        }

        public async Task UpdateStatusByIdAsync(int id, StatusWriteDTO dto)
        {
            var entry = await _statuses.FirstOrDefaultAsync(s => s.Id == id) ??
                throw new NotFoundException("Статус с таким id не найден");

            if (await IsNameExistsAsync(dto.Name, id))
                throw new ConflictException("Статус с таким названием уже существует");

            entry.Name = dto.Name;

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new InternalServerException("Возникла ошибка при обновлении статуса");
            }
        }

        public async Task DeleteStatusByIdAsync(int id)
        {
            var entry = await _statuses.FirstOrDefaultAsync(s => s.Id == id) ??
                throw new NotFoundException("Статус с таким id не найден");

            _statuses.Remove(entry);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new InternalServerException("Возникла ошибка при удалении статуса");
            }
        }

        private async Task<bool> IsNameExistsAsync(string name, int? excludedId = null)
        {
            var query = _statuses.AsQueryable();

            if (excludedId.HasValue)
            {
                query = query.Where(s => s.Id != excludedId.Value);
            }

            return await query.AnyAsync(s => s.Name == name);
        }
    }
}
