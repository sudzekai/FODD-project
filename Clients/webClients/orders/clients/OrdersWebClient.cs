using Clients.helpers;
using Clients.webClients.orders.interfaces;
using Shared.dtos.orders;
using Shared.requests;

namespace Clients.webClients.orders.clients
{
    /// <summary>
    /// Клиент для работы с API заказов.
    /// </summary>
    public class OrdersWebClient : IOrdersWebClient
    {
        private const string _base = "/orders";

        private readonly IWebClient _client;

        public OrdersWebClient(IWebClient client)
        {
            _client = client;
        }

        public Task<List<OrderDTO>> GetOrdersAsync(GetOrdersListRequest req, string token, CancellationToken ct = default)
            => _client.GetAsync<List<OrderDTO>>(
                $"{_base}{QueryBuilder.ToQueryString(req)}",
                token,
                ct) ?? Task.FromResult(new List<OrderDTO>());

        public Task<OrderDTO> GetOrderByIdAsync(int id, string token, CancellationToken ct = default)
            => _client.GetAsync<OrderDTO>($"{_base}/{id}", token, ct);

        public Task UpdateOrderStatusAsync(int id, OrderStatusUpdateDTO dto, string token, CancellationToken ct = default)
            => _client.PutAsync<object?>($"{_base}/{id}/status", dto, token, ct);

        public Task UpdateOrderDeliveryAsync(int id, OrderDeliveryUpdateDTO dto, string token, CancellationToken ct = default)
            => _client.PutAsync<object?>($"{_base}/{id}/delivery", dto, token, ct);

        public Task DeleteOrderAsync(int id, string token, CancellationToken ct = default)
            => _client.DeleteAsync<object?>($"{_base}/{id}", token, ct);

        public Task<int> GetOrdersCountAsync(string token, CancellationToken ct = default)
            => _client.GetAsync<int>($"{_base}/count", token, ct);
    }
}