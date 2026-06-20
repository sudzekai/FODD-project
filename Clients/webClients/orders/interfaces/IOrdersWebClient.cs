using Shared.dtos.orders;
using Shared.requests;

namespace Clients.webClients.orders.interfaces
{
    public interface IOrdersWebClient
    {
        Task DeleteOrderAsync(int id);
        Task<OrderDTO> GetOrderByIdAsync(int id);
        Task<List<OrderDTO>> GetOrdersAsync(GetListRequest req);
        Task<int> GetOrdersCountAsync();
        Task UpdateOrderDeliveryAsync(int id, OrderDeliveryUpdateDTO dto);
        Task UpdateOrderStatusAsync(int id, OrderStatusUpdateDTO dto);
    }
}