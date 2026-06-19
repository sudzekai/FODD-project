using System.ComponentModel.DataAnnotations;

namespace Shared.dtos.products
{
    public class ProductCreateDTO
    {
        public ProductCreateDTO()
        {
            
        }

        public ProductCreateDTO(string name, decimal price, short discount, string unit, int quantity, short? size, string? color, string description, int manufacturerId, int supplierId, int categoryId)
        {
            Name = name;
            Price = price;
            Discount = discount;
            Unit = unit;
            Quantity = quantity;
            Size = size;
            Color = color;
            Description = description;
            ManufacturerId = manufacturerId;
            SupplierId = supplierId;
            CategoryId = categoryId;
        }

        [Required(ErrorMessage = "Name обязателен")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Name должен быть длиной от 5 до 50 символов")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Price обязателен")]
        [Range(1, double.MaxValue, ErrorMessage = "Price должен быть больше 0")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Discount обязателен")]
        [Range(0, 100, ErrorMessage = "Discount должен быть от 0 до 100")]
        public short Discount { get; set; }

        [Required(ErrorMessage = "Unit обязателен")]
        [StringLength(5, MinimumLength = 1, ErrorMessage = "Unit должен быть длиной от 1 до 5 символов")]
        public string Unit { get; set; } = null!;

        [Required(ErrorMessage = "Quantity обязателен")]
        [Range(0, 100, ErrorMessage = "Quantity должен быть от 0 до 100")]
        public int Quantity { get; set; }

        [Range(1, 50, ErrorMessage = "Size должен быть от 1 до 50")]
        public short? Size { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "Color должен быть длиной от 3 до 50 символов")]
        public string? Color { get; set; }

        [Required(ErrorMessage = "Description обязателен")]
        [StringLength(500, MinimumLength = 5, ErrorMessage = "Description должен быть длиной от 5 до 500 символов")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "ManufacturerId обязателен")]
        [Range(0, int.MaxValue, ErrorMessage = "ManufacturerId должен быть больше 0")]
        public int ManufacturerId { get; set; }

        [Required(ErrorMessage = "SupplierId обязателен")]
        [Range(0, int.MaxValue, ErrorMessage = "SupplierId должен быть больше 0")]
        public int SupplierId { get; set; }

        [Required(ErrorMessage = "CategoryId обязателен")]
        [Range(0, int.MaxValue, ErrorMessage = "CategoryId должен быть больше 0")]
        public int CategoryId { get; set; }
    }
}
