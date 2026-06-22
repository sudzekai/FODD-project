using Shared.dtos.products;
using Shared.requests;

namespace Services.products.interfaces
{
    public interface IProductsService
    {
        Task<int> CreateProductAsync(ProductCreateDTO dto);
        Task DeleteProductByIdAsync(int id);
        Task<ProductDTO> GetProductByIdAsync(int id);
        Task<List<ProductSimpleDTO>> GetProductsAsync(GetProductsListRequest req);
        Task<int> GetProductsCountAsync();
        Task UpdateProductByIdAsync(int id, ProductUpdateDTO dto);
        Task UpdateProductPricingByIdAsync(int id, ProductPricingUpdateDTO dto);
        Task UpdateProductRelationsByIdAsync(int id, ProductRelationsUpdateDTO dto);
    }
}