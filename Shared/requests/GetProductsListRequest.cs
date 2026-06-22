using System.ComponentModel.DataAnnotations;

namespace Shared.requests
{
    public class GetProductsListRequest
    {
        public GetProductsListRequest() { }

        public GetProductsListRequest(int offset, int limit, string searchTerm, string orderDirection, string discountsOnly, string color, int size, int categoryId, int manufacturerId, int supplierId)
        {
            Offset = offset;
            Limit = limit;
            SearchTerm = searchTerm;
            OrderDirection = orderDirection;
            DiscountsOnly = discountsOnly;
            Color = color;
            Size = size;
            CategoryId = categoryId;
            ManufacturerId = manufacturerId;
            SupplierId = supplierId;
        }

        [Range(0, int.MaxValue, ErrorMessage = "Offset должен быть больше 0")]
        public int Offset { get; set; } = 0;

        [Range(0, int.MaxValue, ErrorMessage = "Limit должен быть больше 0 и меньше 50")]
        public int Limit { get; set; } = 50;

        public string SearchTerm { get; set; } = "";

        [RegularExpression(@"^?(asc|desc|)", ErrorMessage = "OrderDirection должен быть одним из: asc, desc")]
        public string OrderDirection { get; set; } = "desc";

        public string OrderBy { get; set; } = "";

        [RegularExpression(@"^?(true|false|)", ErrorMessage = "DiscountsOnly должен быть одним из: true, false")]
        public string DiscountsOnly { get; set; } = "";

        public string Color { get; set; } = "";

        public int Size { get; set; } = 0;

        public int CategoryId { get; set; } = 0;

        public int ManufacturerId { get; set; } = 0;

        public int SupplierId { get; set; } = 0;
    }
}
