using Shared.dtos.suppliers;
using Shared.requests;

namespace Clients.webClients.suppliers.interfaces
{
    public interface ISuppliersWebClient
    {
        Task<SupplierDTO> CreateSupplierAsync(SupplierWriteDTO dto, string token, CancellationToken ct = default);
        Task DeleteSupplierAsync(int id, string token, CancellationToken ct = default);
        Task<SupplierDTO> GetSupplierByIdAsync(int id, string token, CancellationToken ct = default);
        Task<List<SupplierDTO>> GetSuppliersAsync(GetListRequest req, string token, CancellationToken ct = default);
        Task<int> GetSuppliersCountAsync(string token, CancellationToken ct = default);
        Task UpdateSupplierAsync(int id, SupplierWriteDTO dto, string token, CancellationToken ct = default);
    }
}