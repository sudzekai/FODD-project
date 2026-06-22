using Shared.dtos.orders;
using Shared.requests;

namespace Clients.webClients.orders.interfaces
{
    public interface IOrdersWebClient
    {
        Task DeleteOrderAsync(int id, string token, CancellationToken ct = default);
        Task<OrderDTO> GetOrderByIdAsync(int id, string token, CancellationToken ct = default);
        Task<List<OrderDTO>> GetOrdersAsync(GetOrdersListRequest req, string token, CancellationToken ct = default);
        Task<int> GetOrdersCountAsync(string token, CancellationToken ct = default);
        Task UpdateOrderDeliveryAsync(int id, OrderDeliveryUpdateDTO dto, string token, CancellationToken ct = default);
        Task UpdateOrderStatusAsync(int id, OrderStatusUpdateDTO dto, string token, CancellationToken ct = default);
    }
}