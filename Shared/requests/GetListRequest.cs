using System.ComponentModel.DataAnnotations;

namespace Shared.requests
{
    public class GetListRequest
    {
        public GetListRequest() { }

        public GetListRequest(int offset, int limit, string searchTerm, string orderDirection)
        {
            Offset = offset;
            Limit = limit;
            SearchTerm = searchTerm;
            OrderDirection = orderDirection;
        }

        [Range(0, int.MaxValue, ErrorMessage = "Offset должен быть больше 0")]
        public int Offset { get; set; } = 0;

        [Range(0, int.MaxValue, ErrorMessage = "Limit должен быть больше 0 и меньше 50")]
        public int Limit { get; set; } = 50;

        public string SearchTerm { get; set; } = "";

        [RegularExpression(@"^?(asc|desc|)", ErrorMessage = "OrderDirection должен быть одним из: asc, desc")]
        public string OrderDirection { get; set; } = "desc";
    }
}
