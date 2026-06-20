using Shared.dtos.products;

namespace Clients.webClients.products.interfaces
{
    public interface IProductTagsWebClient
    {
        Task<bool> AddProductTagsAsync(int productId, ProductTagsUpdateDTO dto);
        Task<List<string>> GetProductTagsAsync(int productId);
        Task<int> GetProductTagsCountAsync(int productId);
        Task<bool> RemoveProductTagsAsync(int productId, ProductTagsUpdateDTO dto);
    }
}