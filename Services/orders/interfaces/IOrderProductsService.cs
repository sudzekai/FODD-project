using Shared.dtos.orders;

namespace Services.orders.services
{
    public interface IOrderProductsService
    {
        Task AddOrderProductByOrderIdAsync(int orderId, OrderProductUpdateDTO dto);
        Task DeleteOrderProductByOrderIdAsync(int orderId, OrderProductUpdateDTO dto);
        Task<List<OrderProductDTO>> GetOrderProductsByOrderIdAsync(int orderId);
        Task<int> GetOrderProductsCountByOrderIdAsync(int id);
        Task<int> GetOrderProductsSumCountByOrderIdAsync(int id);
        Task RemoveOrderProductByOrderIdAsync(int orderId, OrderProductUpdateDTO dto);
    }
}