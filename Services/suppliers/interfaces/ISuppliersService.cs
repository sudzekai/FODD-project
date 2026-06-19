using Shared.dtos.suppliers;
using Shared.requests;

namespace Services.suppliers.interfaces
{
    public interface ISuppliersService
    {
        Task<int> CreateSupplierAsync(SupplierWriteDTO dto);
        Task DeleteSupplierByIdAsync(int id);
        Task<SupplierDTO> GetSupplierByIdAsync(int id);
        Task<List<SupplierDTO>> GetSuppliersAsync(GetListRequest req);
        Task<int> GetSuppliersCountAsync();
        Task UpdateSupplierByIdAsync(int id, SupplierWriteDTO dto);
    }
}
