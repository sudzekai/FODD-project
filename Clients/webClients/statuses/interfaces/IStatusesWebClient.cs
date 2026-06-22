using Shared.dtos.statuses;

namespace Clients.webClients.statuses.interfaces
{
    public interface IStatusesWebClient
    {
        Task<StatusDTO> CreateStatusAsync(StatusWriteDTO dto, string token, CancellationToken ct = default);
        Task DeleteStatusAsync(int id, string token, CancellationToken ct = default);
        Task<StatusDTO> GetStatusByIdAsync(int id, string token, CancellationToken ct = default);
        Task<List<StatusDTO>> GetStatusesAsync(string token, CancellationToken ct = default);
        Task<int> GetStatusesCountAsync(string token, CancellationToken ct = default);
        Task UpdateStatusAsync(int id, StatusWriteDTO dto, string token, CancellationToken ct = default);
    }
}