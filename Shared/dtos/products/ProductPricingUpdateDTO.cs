using System.ComponentModel.DataAnnotations;

namespace Shared.dtos.products
{
    public class ProductPricingUpdateDTO
    {
        public ProductPricingUpdateDTO()
        {
            
        }

        public ProductPricingUpdateDTO(decimal price, short discount)
        {
            Price = price;
            Discount = discount;
        }

        [Required(ErrorMessage = "Price обязателен")]
        [Range(1, double.MaxValue, ErrorMessage = "Price должен быть больше 0")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Discount обязателен")]
        [Range(0, 100, ErrorMessage = "Discount должен быть от 0 до 100")]
        public short Discount { get; set; }
    }
}
