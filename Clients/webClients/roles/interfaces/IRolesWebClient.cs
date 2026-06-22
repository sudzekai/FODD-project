using Shared.dtos.roles;
using Shared.requests;

namespace Clients.webClients.roles.interfaces
{
    public interface IRolesWebClient
    {
        Task<RoleDTO> GetRoleByIdAsync(int id, string token, CancellationToken ct = default);
        Task<List<RoleDTO>> GetRolesAsync(GetListRequest req, string token, CancellationToken ct = default);
        Task<int> GetRolesCountAsync(string token, CancellationToken ct = default);
    }
}