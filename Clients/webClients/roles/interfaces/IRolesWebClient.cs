using Shared.dtos.roles;
using Shared.requests;

namespace Clients.webClients.roles.interfaces
{
    public interface IRolesWebClient
    {
        Task<RoleDTO> GetRoleByIdAsync(int id);
        Task<List<RoleDTO>> GetRolesAsync(GetListRequest req);
        Task<int> GetRolesCountAsync();
    }
}