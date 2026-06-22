using Shared.dtos.tags;
using Shared.requests;

namespace Clients.webClients.tags.interfaces
{
    public interface ITagsWebClient
    {
        Task<TagDTO> CreateTagAsync(TagWriteDTO dto, string token, CancellationToken ct = default);
        Task DeleteTagAsync(int id, string token, CancellationToken ct = default);
        Task<TagDTO> GetTagByIdAsync(int id, string token, CancellationToken ct = default);
        Task<List<TagDTO>> GetTagsAsync(GetListRequest req, string token, CancellationToken ct = default);
        Task<int> GetTagsCountAsync(string token, CancellationToken ct = default);
        Task UpdateTagAsync(int id, TagWriteDTO dto, string token, CancellationToken ct = default);
    }
}