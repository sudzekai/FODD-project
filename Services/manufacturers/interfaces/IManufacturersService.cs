using Shared.dtos.manufacturers;
using Shared.requests;

namespace Services.manufacturers.interfaces
{
    public interface IManufacturersService
    {
        Task<int> CreateManufacturerAsync(ManufacturerWriteDTO dto);
        Task DeleteManufacturerByIdAsync(int id);
        Task<ManufacturerDTO> GetManufacturerByIdAsync(int id);
        Task<List<ManufacturerDTO>> GetManufacturersAsync(GetListRequest req);
        Task<int> GetManufacturersCountAsync();
        Task UpdateManufacturerByIdAsync(int id, ManufacturerWriteDTO dto);
    }
}
