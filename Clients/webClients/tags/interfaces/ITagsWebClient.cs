using Shared.dtos.tags;
using Shared.requests;

namespace Clients.webClients.tags.interfaces
{
    public interface ITagsWebClient
    {
        Task<TagDTO> CreateTagAsync(TagWriteDTO dto);
        Task DeleteTagAsync(int id);
        Task<TagDTO> GetTagByIdAsync(int id);
        Task<List<TagDTO>> GetTagsAsync(GetListRequest req);
        Task<int> GetTagsCountAsync();
        Task UpdateTagAsync(int id, TagWriteDTO dto);
    }
}