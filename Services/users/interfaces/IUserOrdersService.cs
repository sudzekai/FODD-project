using Shared.dtos.orders;

namespace Services.users.interfaces
{
    public interface IUserOrdersService
    {
        Task<int> CreateOrderByUserId(int id);
        Task<List<OrderSimpleDTO>> GetUserOrdersByUserIdAsync(int id);
        Task<int> GetUserOrdersCountByUserIdAsync(int id);
    }
}