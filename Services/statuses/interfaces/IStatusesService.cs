using Shared.dtos.statuses;

namespace Services.statuses.interfaces
{
    public interface IStatusesService
    {
        Task<int> CreateStatusAsync(StatusWriteDTO dto);
        Task DeleteStatusByIdAsync(int id);
        Task<StatusDTO> GetStatusByIdAsync(int id);
        Task<List<StatusDTO>> GetStatusesAsync();
        Task UpdateStatusByIdAsync(int id, StatusWriteDTO dto);
    }
}