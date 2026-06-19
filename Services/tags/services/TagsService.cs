using DB.dbContext;
using DB.models;
using Microsoft.EntityFrameworkCore;
using Services.tags.interfaces;
using Services.unitOfWork;
using Shared.dtos.tags;
using Shared.requests;
using Shared.types.exceptions;

namespace Services.tags.services
{


    public class TagsService : ITagsService
    {
        private readonly DbSet<Tag> _tags;
        private readonly IUnitOfWork _uow;

        public TagsService(ShoesStoreDbContext ctx, IUnitOfWork uow)
        {
            _tags = ctx.Tags;
            _uow = uow;
        }

        public async Task<List<TagDTO>> GetTagsAsync(GetListRequest req)
        {
            var query = _tags.AsQueryable();

            if (!string.IsNullOrWhiteSpace(req.SearchTerm))
            {
                query = query.Where(
                    t => t.Name.Contains(req.SearchTerm)
                );
            }

            query = req.OrderDirection switch
            {
                "asc" => query.OrderBy(t => t.Id),
                _ => query.OrderByDescending(t => t.Id)

            };

            query = query.Skip(req.Offset)
                         .Take(req.Limit);

            var result = await query.Select(
                t => new TagDTO(t.Id, t.Name)
            ).ToListAsync();

            return result;
        }

        public async Task<TagDTO> GetTagByIdAsync(int id)
        {
            var query = _tags.AsQueryable();

            var entry = await query.FirstOrDefaultAsync(t => t.Id == id)
                ?? throw new NotFoundException("Тэг с таким id не найден");

            var result = new TagDTO(
                entry.Id,
                entry.Name
            );

            return result;
        }

        public async Task<int> CreateTagAsync(TagWriteDTO dto)
        {
            var query = _tags.AsQueryable();

            if (await IsNameExists(dto.Name))
                throw new ConflictException("Тэг с таким названием уже существует");

            var tag = new Tag()
            {
                Name = dto.Name,
            };

            var created = await _tags.AddAsync(tag);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new InternalServerException($"Возникла ошибка при создании тэга");
            }

            return created.Entity.Id;
        }

        public async Task UpdateTagByIdAsync(int id, TagWriteDTO dto)
        {
            var query = _tags.AsQueryable();

            var existing = await query.FirstOrDefaultAsync(t => t.Id == id)
                ?? throw new NotFoundException("Тэг с таким id не найден");

            if (existing.Name == dto.Name)
                throw new BadRequestException("Исходные данные совпадают с переданными");

            if (await IsNameExists(dto.Name, id))
                throw new ConflictException("Тэг с таким названием уже существует");

            existing.Name = dto.Name;

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new InternalServerException("Возникла ошибка при обновлении данных тэга");
            }
        }

        public async Task DeleteTagByIdAsync(int id)
        {
            var query = _tags.AsQueryable();

            var existing = await query.FirstOrDefaultAsync(t => t.Id == id)
                ?? throw new NotFoundException("Тэг с таким id не найден");

            _tags.Remove(existing);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch
            {
                throw new InternalServerException("Возникла ошибка при удалении тэга");
            }
        }

        private async Task<bool> IsNameExists(string name, int? excludeTagId = null)
        {
            var query = _tags.AsQueryable();

            if (excludeTagId.HasValue)
                query = query.Where(t => t.Id != excludeTagId.Value);

            return await query.AnyAsync(t => t.Name == name);
        }

        public async Task<int> GetTagsCountAsync() => await _tags.CountAsync();
    }
}
