using System.ComponentModel.DataAnnotations;

namespace Shared.dtos.orders
{
    public class OrderStatusUpdateDTO
    {
        public OrderStatusUpdateDTO()
        {
            
        }

        public OrderStatusUpdateDTO(int statusId)
        {
            StatusId = statusId;
        }

        [Required(ErrorMessage = "StatusId обязателен")]
        [Range(1, int.MaxValue, ErrorMessage = "StatusId должен быть больше 0")]
        public int StatusId { get; set; }
    }
}
