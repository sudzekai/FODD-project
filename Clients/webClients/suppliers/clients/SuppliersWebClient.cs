using Clients.helpers;
using Clients.webClients.suppliers.interfaces;
using Shared.dtos.suppliers;
using Shared.requests;

namespace Clients.webClients.suppliers.clients
{
    /// <summary>
    /// HTTP-клиент для работы с API поставщиков.
    /// </summary>
    public class SuppliersWebClient : ISuppliersWebClient
    {
        private const string _base = "/suppliers";

        private readonly IWebClient _client;

        public SuppliersWebClient(IWebClient client)
        {
            _client = client;
        }

        public Task<List<SupplierDTO>> GetSuppliersAsync(GetListRequest req, string token, CancellationToken ct = default)
            => _client.GetAsync<List<SupplierDTO>>(
                $"{_base}{QueryBuilder.ToQueryString(req)}",
                token,
                ct) ?? Task.FromResult(new List<SupplierDTO>());

        public Task<SupplierDTO> GetSupplierByIdAsync(int id, string token, CancellationToken ct = default)
            => _client.GetAsync<SupplierDTO>($"{_base}/{id}", token, ct);

        public Task<SupplierDTO> CreateSupplierAsync(SupplierWriteDTO dto, string token, CancellationToken ct = default)
            => _client.PostAsync<SupplierDTO>(_base, dto, token, ct);

        public Task UpdateSupplierAsync(int id, SupplierWriteDTO dto, string token, CancellationToken ct = default)
            => _client.PutAsync<object?>($"{_base}/{id}", dto, token, ct);

        public Task DeleteSupplierAsync(int id, string token, CancellationToken ct = default)
            => _client.DeleteAsync<object?>($"{_base}/{id}", token, ct);

        public Task<int> GetSuppliersCountAsync(string token, CancellationToken ct = default)
            => _client.GetAsync<int>($"{_base}/count", token, ct);
    }
}