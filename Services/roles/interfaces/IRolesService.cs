using Shared.dtos.roles;
using Shared.requests;

namespace Services.roles.interfaces
{
    public interface IRolesService
    {
        Task<RoleDTO> GetRoleByIdAsync(int id);
        Task<List<RoleDTO>> GetRolesAsync(GetListRequest req);
    }
}
