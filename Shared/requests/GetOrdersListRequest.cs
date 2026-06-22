using System.ComponentModel.DataAnnotations;

namespace Shared.requests
{
    public class GetOrdersListRequest
    {
        [Range(0, int.MaxValue, ErrorMessage = "Offset должен быть больше 0")]
        public int Offset { get; set; } = 0;

        [Range(0, int.MaxValue, ErrorMessage = "Limit должен быть больше 0 и меньше 50")]
        public int Limit { get; set; } = 50;

        public string SearchTerm { get; set; } = "";

        [RegularExpression(@"^?(asc|desc|)", ErrorMessage = "OrderDirection должен быть одним из: asc, desc")]
        public string OrderDirection { get; set; } = "desc";

        public string OrderBy { get; set; } = "";

        public int StatusId { get; set; } = 0;

        public DateTime? CreationDateTimeStart { get; set; } = null;
        public DateTime? CreationDateTimeEnd { get; set; } = null;

        public DateTime? DeliveryDateTimeStart { get; set; } = null;
        public DateTime? DeliveryDateTimeEnd { get; set; } = null;
    }
}
