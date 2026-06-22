using Shared.dtos.categories;
using Shared.requests;

namespace Services.categories.interfaces
{
    public interface ICategoriesService
    {
        Task<int> CreateCategoryAsync(CategoryWriteDTO dto);
        Task DeleteCategoryByIdAsync(int id);
        Task<List<CategoryDTO>> GetCategoriesAsync(GetListRequest req);
        Task<int> GetCategoriesCountAsync();
        Task<CategoryDTO> GetCategoryByIdAsync(int id);
        Task UpdateCategoryByIdAsync(int id, CategoryWriteDTO dto);
    }
}