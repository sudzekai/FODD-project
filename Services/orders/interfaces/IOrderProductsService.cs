using Shared.dtos.orders;

namespace Services.orders.interfaces
{
    public interface IOrderProductsService
    {
        Task AddOrderProductByUserIdAsync(int userId, OrderProductUpdateDTO dto);
        Task DeleteOrderProductByUserIdAsync(int userId, OrderProductUpdateDTO dto);
        Task<List<OrderProductDTO>> GetOrderProductsByOrderIdAsync(int orderId);
        Task<int> GetOrderProductsCountByOrderIdAsync(int id);
        Task<int> GetOrderProductsSumCountByOrderIdAsync(int id);
        Task RemoveOrderProductByUserIdAsync(int userId, OrderProductUpdateDTO dto);
    }
}