using DB.dbContext;
using DB.models;
using Microsoft.EntityFrameworkCore;
using Services.products.interfaces;
using Services.unitOfWork;
using Shared.dtos.products;
using Shared.requests;
using Shared.types.exceptions;

namespace Services.products.services
{
    public class ProductsService : IProductsService
    {
        private readonly DbSet<Product> _products;
        private readonly IUnitOfWork _uow;

        public ProductsService(ShoesStoreDbContext ctx, IUnitOfWork uow)
        {
            _products = ctx.Products;
            _uow = uow;
        }

        public async Task<List<ProductSimpleDTO>> GetProductsAsync(GetProductsListRequest req)
        {
            var query = _products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(req.SearchTerm))
            {
                query = query.Include(p => p.Tags).Where(p =>
                    p.Name.Contains(req.SearchTerm)
                    || p.Article.Contains(req.SearchTerm)
                    || p.Tags.Any(t => t.Name.Contains(req.SearchTerm))
                );
            }

            if (req.OrderDirection == "asc")
            {
                if (req.OrderBy == "price")
                    query = query.OrderBy(p => p.Price);
                else if (req.OrderBy == "discount")
                    query = query.OrderBy(p => p.Discount);
                else
                    query = query.OrderBy(p => p.Id);
            }
            else
            {
                if (req.OrderBy == "price")
                    query = query.OrderByDescending(p => p.Price);
                else if (req.OrderBy == "discount")
                    query = query.OrderByDescending(p => p.Discount);
                else
                    query = query.OrderByDescending(p => p.Id);
            }

            if (req.DiscountsOnly == "true")
                query = query.Where(p => p.Discount > 0);

            if (!string.IsNullOrWhiteSpace(req.Color))
                query = query.Where(p => p.Color.Contains(req.Color));

            if (req.Size > 0)
                query = query.Where(p => p.Size == req.Size);

            if (req.ManufacturerId > 0)
                query = query.Where(p => p.ManufacturerId == req.ManufacturerId);

            if (req.CategoryId > 0)
                query = query.Where(p => p.CategoryId == req.CategoryId);

            if (req.SupplierId > 0)
                query = query.Where(p => p.SupplierId == req.SupplierId);


            query = query.Skip(req.Offset).Take(req.Limit);

            return await query
                .Select(p => new ProductSimpleDTO(p.Id, p.Article, p.Name, p.Price, p.Discount))
                .ToListAsync();
        }

        public async Task<ProductDTO> GetProductByIdAsync(int id)
        {
            var entry = await _products
                .FirstOrDefaultAsync(p => p.Id == id)
                ?? throw new NotFoundException("Товар с таким id не найден");

            return new ProductDTO(
                entry.Id,
                entry.Article,
                entry.Name,
                entry.Price,
                entry.Discount,
                entry.Unit,
                entry.Quantity,
                entry.Size,
                entry.Color,
                entry.Description,
                entry.ManufacturerId,
                entry.SupplierId,
                entry.CategoryId,
                [.. entry.Tags.Select(t => t.Id)]
            );
        }

        public async Task<int> CreateProductAsync(ProductCreateDTO dto)
        {
            string article;

            do
            {
                article = GenerateArticle();
            }
            while (await IsArticleExists(article));

            var product = new Product
            {
                Name = dto.Name,
                Article = article,
                Price = dto.Price,
                Discount = dto.Discount,
                Unit = dto.Unit,
                Quantity = dto.Quantity,
                Size = dto.Size,
                Color = dto.Color,
                Description = dto.Description,
                ManufacturerId = dto.ManufacturerId,
                SupplierId = dto.SupplierId,
                CategoryId = dto.CategoryId
            };

            await _products.AddAsync(product);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch
            {
                throw new InternalServerException("Возникла ошибка при создании товара");
            }

            return product.Id;
        }

        public async Task UpdateProductByIdAsync(int id, ProductUpdateDTO dto)
        {
            var existing = await _products
                .FirstOrDefaultAsync(p => p.Id == id)
                ?? throw new NotFoundException("Товар с таким id не найден");

            if (existing.Name == dto.Name
                && existing.Unit == dto.Unit
                && existing.Quantity == dto.Quantity
                && existing.Size == dto.Size
                && existing.Color == dto.Color
                && existing.Description == dto.Description
                && existing.CategoryId == dto.CategoryId)
                throw new BadRequestException("Исходные данные совпадают с переданными");

            existing.Name = dto.Name;
            existing.Unit = dto.Unit;
            existing.Quantity = dto.Quantity;
            existing.Size = dto.Size;
            existing.Color = dto.Color;
            existing.Description = dto.Description;
            existing.CategoryId = dto.CategoryId;

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch
            {
                throw new InternalServerException("Возникла ошибка при обновлении товара");
            }
        }

        public async Task UpdateProductPricingByIdAsync(int id, ProductPricingUpdateDTO dto)
        {
            var existing = await _products
                .FirstOrDefaultAsync(p => p.Id == id)
                ?? throw new NotFoundException("Товар с таким id не найден");

            if (existing.Price == dto.Price && existing.Discount == dto.Discount)
                throw new BadRequestException("Исходные данные совпадают с переданными");

            existing.Price = dto.Price;
            existing.Discount = dto.Discount;

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch
            {
                throw new InternalServerException("Ошибка при обновлении цены товара");
            }
        }

        public async Task UpdateProductRelationsByIdAsync(int id, ProductRelationsUpdateDTO dto)
        {
            var existing = await _products
                .FirstOrDefaultAsync(p => p.Id == id)
                ?? throw new NotFoundException("Товар с таким id не найден");

            if (existing.SupplierId == dto.SupplierId &&
                existing.ManufacturerId == dto.ManufacturerId)
                throw new BadRequestException("Исходные данные совпадают с переданными");

            existing.SupplierId = dto.SupplierId;
            existing.ManufacturerId = dto.ManufacturerId;

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch
            {
                throw new InternalServerException("Возникла ошибка при обновлении данных товара");
            }
        }

        public async Task DeleteProductByIdAsync(int id)
        {
            var existing = await _products
                .FirstOrDefaultAsync(p => p.Id == id)
                ?? throw new NotFoundException("Товар с таким id не найден");

            _products.Remove(existing);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch
            {
                throw new InternalServerException("Возникла ошибка при удалении товара");
            }
        }

        private static string GenerateArticle()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            return new([.. Enumerable.Range(0, 6).Select(_ => chars[Random.Shared.Next(chars.Length)])]);
        }

        private async Task<bool> IsArticleExists(string article) => await _products.AnyAsync(p => p.Article == article);

        public async Task<int> GetProductsCountAsync() => await _products.CountAsync();
    }
}