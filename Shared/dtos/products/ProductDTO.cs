namespace Shared.dtos.products
{
    public class ProductDTO
    {
        public ProductDTO()
        {
            
        }

        public ProductDTO(int id, string article, string name, decimal price, short discount, string unit, int quantity, short? size, string? color, string description, int? manufacturerId, int? supplierId, int? categoryId, List<int> tagIds)
        {
            Id = id;
            Article = article;
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
            TagIds = tagIds;
        }

        public int Id { get; set; }

        public string Article { get; set; } = null!;

        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public short Discount { get; set; }

        public string Unit { get; set; } = null!;

        public int Quantity { get; set; }

        public short? Size { get; set; }

        public string? Color { get; set; }

        public string Description { get; set; } = null!;

        public int? ManufacturerId { get; set; }

        public int? SupplierId { get; set; }

        public int? CategoryId { get; set; }

        public List<int> TagIds { get; set; }
    }
}
