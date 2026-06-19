using Shared.dtos.orders;
using Shared.requests;

namespace Services.orders.interfaces
{
    public interface IOrdersService
    {
        Task DeleteOrderByIdAsync(int id);
        Task<OrderDTO> GetOrderByIdAsync(int id);
        Task<List<OrderSimpleDTO>> GetOrdersAsync(GetListRequest req);
        Task<int> GetOrdersCountAsync();
        Task UpdateOrderDeliveryByOrderIdAsync(int id, OrderDeliveryUpdateDTO dto);
        Task UpdateOrderStatusByOrderIdAsync(int id, OrderStatusUpdateDTO dto);
    }
}