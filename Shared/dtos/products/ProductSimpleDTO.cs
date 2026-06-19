namespace Shared.dtos.products
{
    public class ProductSimpleDTO
    {
        public ProductSimpleDTO()
        {
            
        }

        public ProductSimpleDTO(int id, string article, string name, decimal price, short discount)
        {
            Id = id;
            Article = article;
            Name = name;
            Price = price;
            Discount = discount;
        }

        public int Id { get; set; }

        public string Article { get; set; } = null!;

        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public short Discount { get; set; }
    }
}
