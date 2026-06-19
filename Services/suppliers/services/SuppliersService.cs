using DB.dbContext;
using DB.models;
using Microsoft.EntityFrameworkCore;
using Services.suppliers.interfaces;
using Services.unitOfWork;
using Shared.dtos.suppliers;
using Shared.requests;
using Shared.types.exceptions;

namespace Services.suppliers.services
{
    public class SuppliersService : ISuppliersService
    {
        private readonly DbSet<Supplier> _suppliers;
        private readonly IUnitOfWork _uow;

        public SuppliersService(ShoesStoreDbContext ctx, IUnitOfWork uow)
        {
            _suppliers = ctx.Suppliers;
            _uow = uow;
        }

        public async Task<List<SupplierDTO>> GetSuppliersAsync(GetListRequest req)
        {
            var query = _suppliers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(req.SearchTerm))
            {
                query = query.Where(
                    s => s.Name.Contains(req.SearchTerm)
                );
            }

            query = req.OrderDirection switch
            {
                "asc" => query.OrderBy(s => s.Id),
                _ => query.OrderByDescending(s => s.Id)

            };

            query = query.Skip(req.Offset)
                         .Take(req.Limit);

            var result = await query.Select(
                s => new SupplierDTO(s.Id, s.Name)
            ).ToListAsync();

            return result;
        }

        public async Task<SupplierDTO> GetSupplierByIdAsync(int id)
        {
            var query = _suppliers.AsQueryable();

            var entry = await query.FirstOrDefaultAsync(s => s.Id == id)
                ?? throw new NotFoundException("Поставщик с таким id не найден");

            var result = new SupplierDTO(
                entry.Id,
                entry.Name
            );

            return result;
        }

        public async Task<int> CreateSupplierAsync(SupplierWriteDTO dto)
        {
            var query = _suppliers.AsQueryable();

            if (await IsNameExists(dto.Name))
                throw new ConflictException("Поставщик с таким названием уже существует");

            var supplier = new Supplier()
            {
                Name = dto.Name,
            };

            var created = await _suppliers.AddAsync(supplier);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new InternalServerException($"Возникла ошибка при создании поставщика");
            }

            return created.Entity.Id;
        }

        public async Task UpdateSupplierByIdAsync(int id, SupplierWriteDTO dto)
        {
            var query = _suppliers.AsQueryable();

            var existing = await query.FirstOrDefaultAsync(s => s.Id == id)
                ?? throw new NotFoundException("Поставщик с таким id не найден");

            if (existing.Name == dto.Name)
                throw new BadRequestException("Исходные данные совпадают с переданными");

            if (await IsNameExists(dto.Name, id))
                throw new ConflictException("Поставщик с таким названием уже существует");

            existing.Name = dto.Name;

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new InternalServerException("Возникла ошибка при обновлении данных поставщика");
            }
        }

        public async Task DeleteSupplierByIdAsync(int id)
        {
            var query = _suppliers.AsQueryable();

            var existing = await query.FirstOrDefaultAsync(s => s.Id == id)
                ?? throw new NotFoundException("Поставщик с таким id не найден");

            _suppliers.Remove(existing);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch
            {
                throw new InternalServerException("Возникла ошибка при удалении поставщика");
            }
        }

        public async Task<int> GetSuppliersCountAsync()
        {
            var query = _suppliers.AsQueryable();
            return await query.CountAsync();
        }

        private async Task<bool> IsNameExists(string name, int? excludeSupplierId = null)
        {
            var query = _suppliers.AsQueryable();

            if (excludeSupplierId.HasValue)
                query = query.Where(s => s.Id != excludeSupplierId.Value);

            return await query.AnyAsync(s => s.Name == name);
        }
    }
}
