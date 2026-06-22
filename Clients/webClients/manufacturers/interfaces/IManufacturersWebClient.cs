using Shared.dtos.manufacturers;
using Shared.requests;

namespace Clients.webClients.manufacturers.interfaces
{
    public interface IManufacturersWebClient
    {
        Task<ManufacturerDTO> CreateManufacturerAsync(ManufacturerWriteDTO dto, string token, CancellationToken ct = default);
        Task DeleteManufacturerAsync(int id, string token, CancellationToken ct = default);
        Task<ManufacturerDTO> GetManufacturerByIdAsync(int id, string token, CancellationToken ct = default);
        Task<List<ManufacturerDTO>> GetManufacturersAsync(GetListRequest req, string token, CancellationToken ct = default);
        Task<int> GetManufacturersCountAsync(string token, CancellationToken ct = default);
        Task UpdateManufacturerAsync(int id, ManufacturerWriteDTO dto, string token, CancellationToken ct = default);
    }
}