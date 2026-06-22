using Clients.webClients.products.interfaces;
using Shared.dtos.products;
using Shared.dtos.tags;

namespace Clients.webClients.products.clients
{
    /// <summary>
    /// Клиент для работы с тегами товаров.
    /// </summary>
    public class ProductTagsWebClient : IProductTagsWebClient
    {
        private const string _base = "/products";

        private readonly IWebClient _client;

        public ProductTagsWebClient(IWebClient client)
        {
            _client = client;
        }

        public Task<List<TagDTO>> GetProductTagsAsync(int productId, string token, CancellationToken ct = default)
            => _client.GetAsync<List<TagDTO>>($"{_base}/{productId}/tags", token, ct);

        public Task AddProductTagsAsync(int productId, ProductTagsUpdateDTO dto, string token, CancellationToken ct = default)
            => _client.PostAsync<object?>($"{_base}/{productId}/tags/add", dto, token, ct);

        public Task RemoveProductTagsAsync(int productId, ProductTagsUpdateDTO dto, string token, CancellationToken ct = default)
            => _client.PostAsync<object?>($"{_base}/{productId}/tags/remove", dto, token, ct);

        public Task<int> GetProductTagsCountAsync(int productId, string token, CancellationToken ct = default)
            => _client.GetAsync<int>($"{_base}/{productId}/tags/count", token, ct);
    }
}