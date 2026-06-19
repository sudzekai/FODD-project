using System.ComponentModel.DataAnnotations;

namespace Shared.dtos.orders
{
    public class OrderCreateDTO
    {
        public OrderCreateDTO()
        {
            
        }

        public OrderCreateDTO(int userId)
        {
            UserId = userId;
        }

        [Required(ErrorMessage = "UserId обязателен")]
        [Range(1, int.MaxValue, ErrorMessage = "UserId должен быть больше 0")]
        public int UserId { get; set; } 
    }
}
