using DB.dbContext;
using DB.models;
using Microsoft.EntityFrameworkCore;
using Services.products.interfaces;
using Services.unitOfWork;
using Shared.dtos.products;
using Shared.dtos.tags;
using Shared.types.exceptions;

namespace Services.products.services
{
    public class ProductTagsService : IProductTagsService
    {
        private readonly DbSet<Product> _products;
        private readonly DbSet<Tag> _tags;

        private readonly IUnitOfWork _uow;

        public ProductTagsService(ShoesStoreDbContext ctx, IUnitOfWork uow)
        {
            _products = ctx.Products;
            _tags = ctx.Tags;
            _uow = uow;
        }

        public async Task<List<TagDTO>> GetProductTagsByProductIdAsync(int productId)
        {
            var entry = await _products.Include(p => p.Tags).FirstOrDefaultAsync(p => p.Id == productId)
                ?? throw new NotFoundException("Товар с таким id не найден");

            var rawResult = entry.Tags.Select(t =>
                new TagDTO(t.Id, t.Name)
            );

            return rawResult.ToList();
        }

        public async Task AddProductTagByIdAsync(int productId, int id)
        {
            var productEntry = await _products.Include(p => p.Tags).FirstOrDefaultAsync(p => p.Id == productId)
                ?? throw new NotFoundException("Товар с таким id не найден");

            var tagEntry = await _tags.FirstOrDefaultAsync(p => p.Id == id)
                ?? throw new NotFoundException("Тэг с таким id не найден");

            productEntry.Tags.Add(tagEntry);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InternalServerException("Возникла ошибка при добавлении тэга продукту");
            }
        }

        public async Task DeleteProductTagByIdAsync(int productId, int id)
        {
            var productEntry = await _products.Include(p => p.Tags).FirstOrDefaultAsync(p => p.Id == productId)
                ?? throw new NotFoundException("Товар с таким id не найден");

            var tagEntry = await _tags.FirstOrDefaultAsync(p => p.Id == id)
                ?? throw new NotFoundException("Тэг с таким id не найден");

            productEntry.Tags.Remove(tagEntry);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InternalServerException("Возникла ошибка при удалении тэга продукта");
            }
        }

        public async Task AddBatchProductTagsById(int productId, ProductTagsUpdateDTO dto)
        {
            foreach (var tagId in dto.TagIds)
                await AddProductTagByIdAsync(productId, tagId);
        }

        public async Task DeleteBatchProductTagsById(int productId, ProductTagsUpdateDTO dto)
        {
            foreach (var tagId in dto.TagIds)
                await DeleteProductTagByIdAsync(productId, tagId);
        }

        public async Task<int> GetProductTagsCountByProductIdAsync(int id)
        {
            var productEntry = await _products.Include(p => p.Tags).FirstOrDefaultAsync(p => p.Id == id)
                ?? throw new NotFoundException("Товар с таким id не найден");

            return productEntry.Tags.Count;
        }
    }
}
