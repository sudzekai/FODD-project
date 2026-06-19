using Shared.dtos.tags;
using Shared.requests;

namespace Services.tags.interfaces
{
    public interface ITagsService
    {
        Task<int> CreateTagAsync(TagWriteDTO dto);
        Task DeleteTagByIdAsync(int id);
        Task<TagDTO> GetTagByIdAsync(int id);
        Task<List<TagDTO>> GetTagsAsync(GetListRequest req);
        Task<int> GetTagsCountAsync();
        Task UpdateTagByIdAsync(int id, TagWriteDTO dto);
    }
}
