using Shared.dtos.products;
using Shared.requests;

namespace Services.products.interfaces
{
    public interface IProductsService
    {
        Task<int> CreateProductByIdAsync(int id, ProductCreateDTO dto);
        Task DeleteProductByIdAsync(int id);
        Task<ProductDTO> GetProductByIdAsync(int id);
        Task<List<ProductSimpleDTO>> GetProductsAsync(GetListRequest req);
        Task UpdateProductByIdAsync(int id, ProductUpdateDTO dto);
        Task UpdateProductPricingByIdAsync(int id, ProductPricingUpdateDTO dto);
        Task UpdateProductRelationsByIdAsync(int id, ProductRelationsUpdateDTO dto);
    }
}