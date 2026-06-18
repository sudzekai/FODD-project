using Shared.types.enums;
using System.ComponentModel.DataAnnotations;

namespace Shared.requests
{
    public class GetListParameters
    {
        public GetListParameters() { }

        public GetListParameters(int offset, int limit, string searchTerm, string orderDirection)
        {
            Offset = offset;
            Limit = limit;
            SearchTerm = searchTerm;
            OrderDirection = orderDirection;
        }

        public int Offset { get; set; } = 0;

        public int Limit { get; set; } = 50;

        public string SearchTerm { get; set; } = "";

        public string OrderDirection { get; set; } = "desc";
    }
}
