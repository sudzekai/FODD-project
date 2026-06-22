using Clients.helpers;
using Clients.webClients.users.interfaces;
using Shared.dtos.orders;
using Shared.requests;

namespace Clients.webClients.users.clients
{
    /// <summary>
    /// Клиент для обращения к эндпоинтам пользователей, связанным с заказами.
    /// </summary>
    public class UserOrdersWebClient : IUserOrdersWebClient
    {
        private const string _base = "/users";

        private readonly IWebClient _client;

        public UserOrdersWebClient(IWebClient client)
        {
            _client = client;
        }

        public async Task<List<OrderDTO>> GetUserOrdersAsync(int userId, GetOrdersListRequest query, string token, CancellationToken ct = default)
            => await _client.GetAsync<List<OrderDTO>>($"{_base}/{userId}/orders{QueryBuilder.ToQueryString(query)}", token, ct);

        public async Task<int> GetUserOrdersCountAsync(int userId, string token, CancellationToken ct = default)
            => await _client.GetAsync<int>($"{_base}/{userId}/orders/count", token, ct);

        public async Task<OrderDTO> CreateUserOrderAsync(int userId, string token, CancellationToken ct = default)
            => await _client.PostAsync<OrderDTO>($"{_base}/{userId}/orders", null, token, ct);

        public async Task<OrderDTO> GetCartOrderByUserIdAsync(int userId, string token, CancellationToken ct = default)
            => await _client.GetAsync<OrderDTO>($"{_base}/{userId}/cart", token, ct);
    }
}