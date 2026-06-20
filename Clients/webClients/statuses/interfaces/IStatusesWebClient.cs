using Shared.dtos.statuses;

namespace Clients.webClients.statuses.interfaces
{
    public interface IStatusesWebClient
    {
        Task<StatusDTO> CreateStatusAsync(StatusWriteDTO dto);
        Task DeleteStatusAsync(int id);
        Task<StatusDTO> GetStatusByIdAsync(int id);
        Task<List<StatusDTO>> GetStatusesAsync();
        Task<int> GetStatusesCountAsync();
        Task UpdateStatusAsync(int id, StatusWriteDTO dto);
    }
}