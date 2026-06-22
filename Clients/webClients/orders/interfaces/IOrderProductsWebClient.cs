using Shared.dtos.orders;

namespace Clients.webClients.orders.interfaces
{
    public interface IOrderProductsWebClient
    {
        Task AddOrderProductAsync(int orderId, OrderProductUpdateDTO dto, string token, CancellationToken ct = default);
        Task DeleteOrderProductAsync(int orderId, OrderProductUpdateDTO dto, string token, CancellationToken ct = default);
        Task<List<OrderProductDTO>> GetOrderProductsAsync(int orderId, string token, CancellationToken ct = default);
        Task<int> GetOrderProductsCountAsync(int orderId, string token, CancellationToken ct = default);
        Task<int> GetOrderProductsSumAsync(int orderId, string token, CancellationToken ct = default);
        Task RemoveOrderProductAsync(int orderId, OrderProductUpdateDTO dto, string token, CancellationToken ct = default);
    }
}