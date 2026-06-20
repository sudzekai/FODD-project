using Shared.dtos.orders;

namespace Clients.webClients.users.interfaces
{
    public interface IUserOrdersWebClient
    {
        Task<OrderDTO> CreateUserOrderAsync(int userId);
        Task<List<OrderDTO>> GetUserOrdersAsync(int userId);
        Task<int> GetUserOrdersCountAsync(int userId);
    }
}