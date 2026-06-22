using Shared.dtos.orders;
using Shared.requests;

namespace Services.users.interfaces
{
    public interface IUserOrdersService
    {
        Task<int> CreateOrderByUserId(int id);
        Task<OrderDTO> GetCartOrderByUserIdAsync(int id);
        Task<List<OrderSimpleDTO>> GetUserOrdersByUserIdAsync(int id, GetOrdersListRequest req);
        Task<int> GetUserOrdersCountByUserIdAsync(int id);
    }
}