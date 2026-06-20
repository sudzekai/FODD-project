using Shared.dtos.products;
using Shared.dtos.tags;

namespace Services.products.interfaces
{
    public interface IProductTagsService
    {
        Task AddBatchProductTagsById(int productId, ProductTagsUpdateDTO dto);
        Task AddProductTagByIdAsync(int productId, int id);
        Task DeleteBatchProductTagsById(int productId, ProductTagsUpdateDTO dto);
        Task DeleteProductTagByIdAsync(int productId, int id);
        Task<List<TagDTO>> GetProductTagsByProductIdAsync(int productId);
        Task<int> GetProductTagsCountByProductIdAsync(int id);
    }
}