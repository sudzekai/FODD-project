using Shared.dtos.categories;
using Shared.requests;

namespace Clients.webClients.categories.interfaces
{
    public interface ICategoriesWebClient
    {
        Task<CategoryDTO> CreateCategoryAsync(CategoryWriteDTO dto, string token, CancellationToken ct = default);
        Task DeleteCategoryAsync(int id, string token, CancellationToken ct = default);
        Task<List<CategoryDTO>> GetCategoriesAsync(GetListRequest req, string token, CancellationToken ct = default);
        Task<int> GetCategoriesCountAsync(string token, CancellationToken ct = default);
        Task<CategoryDTO> GetCategoryByIdAsync(int id, string token, CancellationToken ct = default);
        Task UpdateCategoryAsync(int id, CategoryWriteDTO dto, string token, CancellationToken ct = default);
    }
}