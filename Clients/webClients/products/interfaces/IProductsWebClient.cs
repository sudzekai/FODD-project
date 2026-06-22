using Shared.dtos.products;
using Shared.requests;

namespace Clients.webClients.products.interfaces
{
    public interface IProductsWebClient
    {
        Task<ProductDTO?> CreateProductAsync(ProductCreateDTO dto, string token, CancellationToken ct = default);
        Task DeleteProductAsync(int id, string token, CancellationToken ct = default);
        Task<ProductDTO> GetProductByIdAsync(int id, string token, CancellationToken ct = default);
        Task<List<ProductSimpleDTO>> GetProductsAsync(GetProductsListRequest req, string token, CancellationToken ct = default);
        Task<int> GetProductsCountAsync(string token, CancellationToken ct = default);
        Task UpdateProductAsync(int id, ProductUpdateDTO dto, string token, CancellationToken ct = default);
        Task UpdateProductPricingAsync(int id, ProductPricingUpdateDTO dto, string token, CancellationToken ct = default);
        Task UpdateProductRelationsAsync(int id, ProductRelationsUpdateDTO dto, string token, CancellationToken ct = default);
    }
}