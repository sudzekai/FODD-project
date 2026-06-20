using Shared.dtos.manufacturers;
using Shared.requests;

namespace Clients.webClients.manufacturers.interfaces
{
    public interface IManufacturersWebClient
    {
        Task<ManufacturerDTO> CreateManufacturerAsync(ManufacturerWriteDTO dto);
        Task DeleteManufacturerAsync(int id);
        Task<ManufacturerDTO> GetManufacturerByIdAsync(int id);
        Task<List<ManufacturerDTO>> GetManufacturersAsync(GetListRequest req);
        Task<int> GetManufacturersCountAsync();
        Task UpdateManufacturerAsync(int id, ManufacturerWriteDTO dto);
    }
}