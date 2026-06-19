using Shared.dtos.orders;

namespace Services.orders.interfaces
{
    public interface IOrderProductsService
    {
        Task AddOrderProductByOrderIdAsync(int orderId, OrderProductUpdateDTO dto);
        Task DeleteOrderProductByOrderIdAsync(int orderId, OrderProductUpdateDTO dto);
        Task<List<OrderProductDTO>> GetOrderProductsByOrderIdAsync(int orderId);
        Task RemoveOrderProductByOrderIdAsync(int orderId, OrderProductUpdateDTO dto);
    }
}