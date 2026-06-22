using Shared.dtos.products;
using Shared.dtos.tags;

namespace Clients.webClients.products.interfaces
{
    public interface IProductTagsWebClient
    {
        Task AddProductTagsAsync(int productId, ProductTagsUpdateDTO dto, string token, CancellationToken ct = default);
        Task<List<TagDTO>> GetProductTagsAsync(int productId, string token, CancellationToken ct = default);
        Task<int> GetProductTagsCountAsync(int productId, string token, CancellationToken ct = default);
        Task RemoveProductTagsAsync(int productId, ProductTagsUpdateDTO dto, string token, CancellationToken ct = default);
    }
}