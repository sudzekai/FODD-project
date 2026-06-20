using Shared.dtos.orders;

namespace Clients.webClients.orders.interfaces
{
    public interface IOrderProductsWebClient
    {
        Task AddOrderProductAsync(int orderId, OrderProductUpdateDTO dto);
        Task DeleteOrderProductAsync(int orderId, OrderProductUpdateDTO dto);
        Task<List<OrderProductDTO>> GetOrderProductsAsync(int orderId);
        Task<int> GetOrderProductsCountAsync(int orderId);
        Task<int> GetOrderProductsSumAsync(int orderId);
        Task RemoveOrderProductAsync(int orderId, OrderProductUpdateDTO dto);
    }
}