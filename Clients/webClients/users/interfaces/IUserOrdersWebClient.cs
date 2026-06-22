using Shared.dtos.orders;
using Shared.requests;

namespace Clients.webClients.users.interfaces
{
    public interface IUserOrdersWebClient
    {
        Task<OrderDTO> CreateUserOrderAsync(int userId, string token, CancellationToken ct = default);
        Task<OrderDTO> GetCartOrderByUserIdAsync(int userId, string token, CancellationToken ct = default);
        Task<List<OrderDTO>> GetUserOrdersAsync(int userId, GetOrdersListRequest query, string token, CancellationToken ct = default);
        Task<int> GetUserOrdersCountAsync(int userId, string token, CancellationToken ct = default);
    }
}