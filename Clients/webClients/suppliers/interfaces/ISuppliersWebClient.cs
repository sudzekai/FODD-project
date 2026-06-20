using Shared.dtos.suppliers;
using Shared.requests;

namespace Clients.webClients.suppliers.interfaces
{
    public interface ISuppliersWebClient
    {
        Task<SupplierDTO> CreateSupplierAsync(SupplierWriteDTO dto);
        Task DeleteSupplierAsync(int id);
        Task<SupplierDTO> GetSupplierByIdAsync(int id);
        Task<List<SupplierDTO>> GetSuppliersAsync(GetListRequest req);
        Task<int> GetSuppliersCountAsync();
        Task UpdateSupplierAsync(int id, SupplierWriteDTO dto);
    }
}