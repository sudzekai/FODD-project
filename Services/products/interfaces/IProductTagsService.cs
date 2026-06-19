using Shared.dtos.products;
using Shared.dtos.tags;

namespace Services.products.interfaces
{
    public interface IProductTagsService
    {
        Task AddBatchProductTagsByIdAsync(int productId, ProductTagsUpdateDTO dto);
        Task AddProductTagByIdAsync(int productId, int id);
        Task DeleteBatchProductTagsByIdAsync(int productId, ProductTagsUpdateDTO dto);
        Task DeleteProductTagByIdAsync(int productId, int id);
        Task<List<TagDTO>> GetProductTagsByIdAsync(int productId);
    }
}
