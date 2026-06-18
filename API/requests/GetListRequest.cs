using Shared.types.enums;
using System.ComponentModel.DataAnnotations;

namespace Shared.requests
{
    public class GetListRequest
    {
        public GetListRequest() { }

        public GetListRequest(string offset, string limit, string searchTerm, string orderDirection)
        {
            Offset = offset;
            Limit = limit;
            SearchTerm = searchTerm;
            OrderDirection = orderDirection;
        }

        [RegularExpression(@"^(?!-)(?!0+$)\d+{1,9}$", ErrorMessage = "Offset должен быть числом больше нуля")]
        public string Offset { get; set; } = "0";

        [RegularExpression(@"^([1-9]|[1-5]\d)$", ErrorMessage = "Limit должен быть числом от 1 до 50")]
        public string Limit { get; set; } = "50";

        public string SearchTerm { get; set; } = "";

        [RegularExpression("^?(asc|desc|)$", ErrorMessage = "OrderDirection должен быть asc или desc")]
        public string OrderDirection { get; set; } = "desc";

        public int GetOffset() => int.Parse(Offset);
        public int GetLimit() => int.Parse(Limit);
    }
}
