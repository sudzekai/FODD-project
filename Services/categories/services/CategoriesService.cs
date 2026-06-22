using DB.dbContext;
using DB.models;
using Microsoft.EntityFrameworkCore;
using Services.categories.interfaces;
using Services.unitOfWork;
using Shared.dtos.categories;
using Shared.requests;
using Shared.types.exceptions;

namespace Services.categories.services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly DbSet<Category> _categories;
        private readonly IUnitOfWork _uow;

        public CategoriesService(ShoesStoreDbContext ctx, IUnitOfWork uow)
        {
            _categories = ctx.Categories;
            _uow = uow;
        }

        public async Task<List<CategoryDTO>> GetCategoriesAsync(GetListRequest req)
        {
            var query = _categories.AsQueryable();

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
                m => new CategoryDTO(m.Id, m.Name)
            ).ToListAsync();

            return result;
        }

        public async Task<CategoryDTO> GetCategoryByIdAsync(int id)
        {
            var entry = await _categories.FirstOrDefaultAsync(m => m.Id == id)
                ?? throw new NotFoundException("Категория с таким id не найдена");

            var result = new CategoryDTO(
                entry.Id,
                entry.Name
            );

            return result;
        }

        public async Task<int> CreateCategoryAsync(CategoryWriteDTO dto)
        {
            if (await IsNameExists(dto.Name))
                throw new ConflictException("Категория с таким названием уже существует");

            var category = new Category()
            {
                Name = dto.Name,
            };

            var created = await _categories.AddAsync(category);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new InternalServerException($"Возникла ошибка при создании категории");
            }

            return created.Entity.Id;
        }

        public async Task UpdateCategoryByIdAsync(int id, CategoryWriteDTO dto)
        {
            var existing = await _categories.FirstOrDefaultAsync(m => m.Id == id)
                ?? throw new NotFoundException("Категория с таким id не найдена");

            if (existing.Name == dto.Name)
                throw new BadRequestException("Исходные данные совпадают с переданными");

            if (await IsNameExists(dto.Name, id))
                throw new ConflictException("Категория с таким названием уже существует");

            existing.Name = dto.Name;

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new InternalServerException("Возникла ошибка при обновлении данных категории");
            }
        }

        public async Task DeleteCategoryByIdAsync(int id)
        {
            var existing = await _categories.FirstOrDefaultAsync(m => m.Id == id)
                ?? throw new NotFoundException("Категория с таким id не найдена");

            _categories.Remove(existing);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch
            {
                throw new InternalServerException("Возникла ошибка при удалении категории");
            }
        }

        public async Task<int> GetCategoriesCountAsync() => await _categories.CountAsync();

        private async Task<bool> IsNameExists(string name, int? excludeCategoryId = null)
        {
            var query = _categories.AsQueryable();

            if (excludeCategoryId.HasValue)
                query = query.Where(m => m.Id != excludeCategoryId.Value);

            return await query.AnyAsync(m => m.Name == name);
        }
    }
}
