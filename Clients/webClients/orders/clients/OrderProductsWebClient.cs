using Clients.webClients.orders.interfaces;
using Shared.dtos.orders;

namespace Clients.webClients.orders.clients
{
    public class OrderProductsWebClient : IOrderProductsWebClient
    {
        private const string _base = "/orders";

        private readonly IWebClient _client;

        public OrderProductsWebClient(IWebClient client)
        {
            _client = client;
        }

        public Task<List<OrderProductDTO>> GetOrderProductsAsync(int orderId, string token, CancellationToken ct = default)
            => _client.GetAsync<List<OrderProductDTO>>($"{_base}/{orderId}/products", token, ct);

        public Task<int> GetOrderProductsCountAsync(int orderId, string token, CancellationToken ct = default)
            => _client.GetAsync<int>($"{_base}/{orderId}/products/count", token, ct);

        public Task<int> GetOrderProductsSumAsync(int orderId, string token, CancellationToken ct = default)
            => _client.GetAsync<int>($"{_base}/{orderId}/products/sum", token, ct);

        public Task AddOrderProductAsync(int userId, OrderProductUpdateDTO dto, string token, CancellationToken ct = default)
            => _client.PostAsync<object?>($"{_base}/user/{userId}/products/add", dto, token, ct);

        public Task RemoveOrderProductAsync(int userId, OrderProductUpdateDTO dto, string token, CancellationToken ct = default)
            => _client.PostAsync<object?>($"{_base}/user/{userId}/products/remove", dto, token, ct);

        public Task DeleteOrderProductAsync(int userId, OrderProductUpdateDTO dto, string token, CancellationToken ct = default)
            => _client.PostAsync<object?>($"{_base}/user/{userId}/products/delete", dto, token, ct);
    }
}