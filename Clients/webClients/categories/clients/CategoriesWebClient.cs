using Clients.helpers;
using Clients.webClients.categories.interfaces;
using Shared.dtos.categories;
using Shared.requests;

namespace Clients.webClients.categories.clients
{
    /// <summary>
    /// Клиент для работы с API категорий.
    /// </summary>
    public class CategoriesWebClient : ICategoriesWebClient
    {
        private const string _base = "/categories";

        private readonly IWebClient _client;

        public CategoriesWebClient(IWebClient client)
        {
            _client = client;
        }

        public Task<List<CategoryDTO>> GetCategoriesAsync(GetListRequest req, string token, CancellationToken ct = default)
            => _client.GetAsync<List<CategoryDTO>>(
                $"{_base}{QueryBuilder.ToQueryString(req)}",
                token,
                ct) ?? Task.FromResult(new List<CategoryDTO>());

        public Task<CategoryDTO> GetCategoryByIdAsync(int id, string token, CancellationToken ct = default)
            => _client.GetAsync<CategoryDTO>($"{_base}/{id}", token, ct);

        public Task<CategoryDTO> CreateCategoryAsync(CategoryWriteDTO dto, string token, CancellationToken ct = default)
            => _client.PostAsync<CategoryDTO>(_base, dto, token, ct);

        public Task UpdateCategoryAsync(int id, CategoryWriteDTO dto, string token, CancellationToken ct = default)
            => _client.PutAsync<object?>($"{_base}/{id}", dto, token, ct);

        public Task DeleteCategoryAsync(int id, string token, CancellationToken ct = default)
            => _client.DeleteAsync<object?>($"{_base}/{id}", token, ct);

        public Task<int> GetCategoriesCountAsync(string token, CancellationToken ct = default)
            => _client.GetAsync<int>($"{_base}/count", token, ct);
    }
}