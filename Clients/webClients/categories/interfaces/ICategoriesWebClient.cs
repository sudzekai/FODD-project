using Shared.dtos.categories;
using Shared.requests;

namespace Clients.webClients.categories.interfaces
{
    public interface ICategoriesWebClient
    {
        Task<CategoryDTO> CreateCategoryAsync(CategoryWriteDTO dto);
        Task DeleteCategoryAsync(int id);
        Task<List<CategoryDTO>> GetCategoriesAsync(GetListRequest req);
        Task<int> GetCategoriesCountAsync();
        Task<CategoryDTO> GetCategoryByIdAsync(int id);
        Task UpdateCategoryAsync(int id, CategoryWriteDTO dto);
    }
}