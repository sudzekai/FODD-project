using Clients.helpers;
using Clients.webClients.manufacturers.interfaces;
using Shared.dtos.manufacturers;
using Shared.requests;

namespace Clients.webClients.manufacturers.clients
{
    /// <summary>
    /// Клиент для работы с API производителей.
    /// </summary>
    public class ManufacturersWebClient : IManufacturersWebClient
    {
        private const string _base = "/manufacturers";

        private readonly IWebClient _client;

        public ManufacturersWebClient(IWebClient client)
        {
            _client = client;
        }

        public Task<List<ManufacturerDTO>> GetManufacturersAsync(GetListRequest req, string token, CancellationToken ct = default)
            => _client.GetAsync<List<ManufacturerDTO>>(
                $"{_base}{QueryBuilder.ToQueryString(req)}",
                token,
                ct) ?? Task.FromResult(new List<ManufacturerDTO>());

        public Task<ManufacturerDTO> GetManufacturerByIdAsync(int id, string token, CancellationToken ct = default)
            => _client.GetAsync<ManufacturerDTO>($"{_base}/{id}", token, ct);

        public Task<ManufacturerDTO> CreateManufacturerAsync(ManufacturerWriteDTO dto, string token, CancellationToken ct = default)
            => _client.PostAsync<ManufacturerDTO>(_base, dto, token, ct);

        public Task UpdateManufacturerAsync(int id, ManufacturerWriteDTO dto, string token, CancellationToken ct = default)
            => _client.PutAsync<object?>($"{_base}/{id}", dto, token, ct);

        public Task DeleteManufacturerAsync(int id, string token, CancellationToken ct = default)
            => _client.DeleteAsync<object?>($"{_base}/{id}", token, ct);

        public Task<int> GetManufacturersCountAsync(string token, CancellationToken ct = default)
            => _client.GetAsync<int>($"{_base}/count", token, ct);
    }
}