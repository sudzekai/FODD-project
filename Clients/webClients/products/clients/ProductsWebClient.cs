using Clients.helpers;
using Clients.webClients.products.interfaces;
using Shared.dtos.products;
using Shared.requests;

namespace Clients.webClients.products.clients
{
    /// <summary>
    /// Веб-клиент для работы с товарами.
    /// </summary>
    public class ProductsWebClient : IProductsWebClient
    {
        private const string _base = "/products";

        private readonly IWebClient _client;

        public ProductsWebClient(IWebClient client)
        {
            _client = client;
        }

        public Task<List<ProductSimpleDTO>> GetProductsAsync(GetProductsListRequest req, string token, CancellationToken ct = default)
            => _client.GetAsync<List<ProductSimpleDTO>>(
                $"{_base}{QueryBuilder.ToQueryString(req)}",
                token,
                ct);

        public Task<ProductDTO> GetProductByIdAsync(int id, string token, CancellationToken ct = default)
            => _client.GetAsync<ProductDTO>($"{_base}/{id}", token, ct);

        public Task<ProductDTO?> CreateProductAsync(ProductCreateDTO dto, string token, CancellationToken ct = default)
            => _client.PostAsync<ProductDTO>(_base, dto, token, ct);

        public Task UpdateProductAsync(int id, ProductUpdateDTO dto, string token, CancellationToken ct = default)
            => _client.PutAsync<object?>($"{_base}/{id}", dto, token, ct);

        public Task UpdateProductPricingAsync(int id, ProductPricingUpdateDTO dto, string token, CancellationToken ct = default)
            => _client.PutAsync<object?>($"{_base}/{id}/pricing", dto, token, ct);

        public Task UpdateProductRelationsAsync(int id, ProductRelationsUpdateDTO dto, string token, CancellationToken ct = default)
            => _client.PutAsync<object?>($"{_base}/{id}/relations", dto, token, ct);

        public Task DeleteProductAsync(int id, string token, CancellationToken ct = default)
            => _client.DeleteAsync<object?>($"{_base}/{id}", token, ct);

        public Task<int> GetProductsCountAsync(string token, CancellationToken ct = default)
            => _client.GetAsync<int>($"{_base}/count", token, ct);
    }
}