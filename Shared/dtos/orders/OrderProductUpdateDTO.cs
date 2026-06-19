using System.ComponentModel.DataAnnotations;

namespace Shared.dtos.orders
{
    public class OrderProductUpdateDTO
    {
        public OrderProductUpdateDTO()
        {
            
        }

        public OrderProductUpdateDTO(int productId)
        {
            ProductId = productId;
        }

        [Required(ErrorMessage = "ProductId обязателен")]
        [Range(1, int.MaxValue, ErrorMessage = "ProductId должен быть больше 0")]
        public int ProductId { get; set; }
    }
}
