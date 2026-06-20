using Shared.dtos.products;
using Shared.requests;

namespace Clients.webClients.products.interfaces
{
    public interface IProductsWebClient
    {
        Task<ProductDTO?> CreateProductAsync(ProductCreateDTO dto);
        Task DeleteProductAsync(int id);
        Task<ProductDTO> GetProductByIdAsync(int id);
        Task<List<ProductSimpleDTO>> GetProductsAsync(GetListRequest req);
        Task<int> GetProductsCountAsync();
        Task UpdateProductAsync(int id, ProductUpdateDTO dto);
        Task UpdateProductPricingAsync(int id, ProductPricingUpdateDTO dto);
        Task UpdateProductRelationsAsync(int id, ProductRelationsUpdateDTO dto);
    }
}