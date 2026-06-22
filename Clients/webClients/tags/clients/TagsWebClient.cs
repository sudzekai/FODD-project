using Clients.helpers;
using Clients.webClients.tags.interfaces;
using Shared.dtos.tags;
using Shared.requests;

namespace Clients.webClients.tags.clients
{
    /// <summary>
    /// Веб-клиент для работы с ресурсом тегов.
    /// </summary>
    public class TagsWebClient : ITagsWebClient
    {
        private const string _base = "/tags";

        private readonly IWebClient _client;

        public TagsWebClient(IWebClient client)
        {
            _client = client;
        }

        public Task<List<TagDTO>> GetTagsAsync(GetListRequest req, string token, CancellationToken ct = default)
            => _client.GetAsync<List<TagDTO>>(
                $"{_base}{QueryBuilder.ToQueryString(req)}",
                token,
                ct) ?? Task.FromResult(new List<TagDTO>());

        public Task<TagDTO> GetTagByIdAsync(int id, string token, CancellationToken ct = default)
            => _client.GetAsync<TagDTO>($"{_base}/{id}", token, ct);

        public Task<TagDTO> CreateTagAsync(TagWriteDTO dto, string token, CancellationToken ct = default)
            => _client.PostAsync<TagDTO>(_base, dto, token, ct);

        public Task UpdateTagAsync(int id, TagWriteDTO dto, string token, CancellationToken ct = default)
            => _client.PutAsync<object?>($"{_base}/{id}", dto, token, ct);

        public Task DeleteTagAsync(int id, string token, CancellationToken ct = default)
            => _client.DeleteAsync<object?>($"{_base}/{id}", token, ct);

        public Task<int> GetTagsCountAsync(string token, CancellationToken ct = default)
            => _client.GetAsync<int>($"{_base}/count", token, ct);
    }
}