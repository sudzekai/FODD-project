namespace Shared.dtos.orders
{
    public class OrderProductDTO
    {
        public OrderProductDTO()
        {
        }

        public OrderProductDTO(int id, string article, string name, decimal price, short discount, int quantity)
        {
            Id = id;
            Article = article;
            Name = name;
            Price = price;
            Discount = discount;
            Quantity = quantity;
        }

        public int Id { get; set; }

        public string Article { get; set; } = null!;

        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public short Discount { get; set; }

        public int Quantity { get; set; }
    }
}
