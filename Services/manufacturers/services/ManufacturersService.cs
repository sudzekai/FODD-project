using DB.dbContext;
using DB.models;
using Microsoft.EntityFrameworkCore;
using Services.manufacturers.interfaces;
using Services.unitOfWork;
using Shared.dtos.manufacturers;
using Shared.requests;
using Shared.types.exceptions;

namespace Services.manufacturers.services
{
    public class ManufacturersService : IManufacturersService
    {
        private readonly DbSet<Manufacturer> _manufacturers;
        private readonly IUnitOfWork _uow;

        public ManufacturersService(ShoesStoreDbContext ctx, IUnitOfWork uow)
        {
            _manufacturers = ctx.Manufacturers;
            _uow = uow;
        }

        public async Task<List<ManufacturerDTO>> GetManufacturersAsync(GetListRequest req)
        {
            var query = _manufacturers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(req.SearchTerm))
            {
                query = query.Where(
                    m => m.Name.Contains(req.SearchTerm)
                );
            }

            query = req.OrderDirection switch
            {
                "asc" => query.OrderBy(m => m.Id),
                _ => query.OrderByDescending(m => m.Id)

            };

            query = query.Skip(req.Offset)
                         .Take(req.Limit);

            var result = await query.Select(
                m => new ManufacturerDTO(m.Id, m.Name)
            ).ToListAsync();

            return result;
        }

        public async Task<ManufacturerDTO> GetManufacturerByIdAsync(int id)
        {
            var query = _manufacturers.AsQueryable();

            var entry = await query.FirstOrDefaultAsync(m => m.Id == id)
                ?? throw new NotFoundException("Производитель с таким id не найден");

            var result = new ManufacturerDTO(
                entry.Id,
                entry.Name
            );

            return result;
        }

        public async Task<int> CreateManufacturerAsync(ManufacturerWriteDTO dto)
        {
            var query = _manufacturers.AsQueryable();

            if (await IsNameExists(dto.Name))
                throw new ConflictException("Производитель с таким названием уже существует");

            var manufacturer = new Manufacturer()
            {
                Name = dto.Name,
            };

            var created = await _manufacturers.AddAsync(manufacturer);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new InternalServerException($"Возникла ошибка при создании производителя");
            }

            return created.Entity.Id;
        }

        public async Task UpdateManufacturerByIdAsync(int id, ManufacturerWriteDTO dto)
        {
            var query = _manufacturers.AsQueryable();

            var existing = await query.FirstOrDefaultAsync(m => m.Id == id)
                ?? throw new NotFoundException("Производитель с таким id не найден");

            if (existing.Name == dto.Name)
                throw new BadRequestException("Исходные данные совпадают с переданными");

            if (await IsNameExists(dto.Name, id))
                throw new ConflictException("Производитель с таким названием уже существует");

            existing.Name = dto.Name;

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new InternalServerException("Возникла ошибка при обновлении данных производителя");
            }
        }

        public async Task DeleteManufacturerByIdAsync(int id)
        {
            var query = _manufacturers.AsQueryable();

            var existing = await query.FirstOrDefaultAsync(m => m.Id == id)
                ?? throw new NotFoundException("Производитель с таким id не найден");

            _manufacturers.Remove(existing);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch
            {
                throw new InternalServerException("Возникла ошибка при удалении производителя");
            }
        }

        public async Task<int> GetManufacturersCountAsync() => await _manufacturers.CountAsync();

        private async Task<bool> IsNameExists(string name, int? excludeManufacturerId = null)
        {
            var query = _manufacturers.AsQueryable();

            if (excludeManufacturerId.HasValue)
                query = query.Where(m => m.Id != excludeManufacturerId.Value);

            return await query.AnyAsync(m => m.Name == name);
        }
    }
}
