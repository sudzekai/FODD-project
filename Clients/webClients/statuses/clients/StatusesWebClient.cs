using Clients.webClients.statuses.interfaces;
using Shared.dtos.statuses;

namespace Clients.webClients.statuses.clients
{
    /// <summary>
    /// Клиент для работы со статусами через HTTP API.
    /// </summary>
    public class StatusesWebClient : IStatusesWebClient
    {
        private const string _base = "/statuses";

        private readonly IWebClient _client;

        public StatusesWebClient(IWebClient client)
        {
            _client = client;
        }

        public Task<List<StatusDTO>> GetStatusesAsync(string token, CancellationToken ct = default)
            => _client.GetAsync<List<StatusDTO>>(_base, token, ct);

        public Task<StatusDTO> GetStatusByIdAsync(int id, string token, CancellationToken ct = default)
            => _client.GetAsync<StatusDTO>($"{_base}/{id}", token, ct);

        public Task<StatusDTO> CreateStatusAsync(StatusWriteDTO dto, string token, CancellationToken ct = default)
            => _client.PostAsync<StatusDTO>(_base, dto, token, ct);

        public Task UpdateStatusAsync(int id, StatusWriteDTO dto, string token, CancellationToken ct = default)
            => _client.PutAsync<object?>($"{_base}/{id}", dto, token, ct);

        public Task DeleteStatusAsync(int id, string token, CancellationToken ct = default)
            => _client.DeleteAsync<object?>($"{_base}/{id}", token, ct);

        public Task<int> GetStatusesCountAsync(string token, CancellationToken ct = default)
            => _client.GetAsync<int>($"{_base}/count", token, ct);
    }
}