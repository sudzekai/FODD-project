using Shared.types.enums;

namespace Shared.extensions
{
    public static class OrderDirectionStringExtension
    {
        public static OrderDirection ToOrderDirection(this string str)
            => str switch
            {
                "asc" => OrderDirection.Ascending,
                _ => OrderDirection.Descending,
            };
    }
}
