using Shared.dtos.users;
using Shared.requests;

namespace Clients.webClients.users.interfaces
{
    public interface IUsersWebClient
    {
        Task<UserDTO?> CreateUserAsync(UserCreateDTO dto, string token, CancellationToken ct = default);
        Task DeleteUserAsync(int id, string token, CancellationToken ct = default);
        Task<UserDTO> GetUserByIdAsync(int id, string token, CancellationToken ct = default);
        Task<List<UserSimpleDTO>> GetUsersAsync(GetListRequest req, string token, CancellationToken ct = default);
        Task<int> GetUsersCountAsync(string token, CancellationToken ct = default);
        Task UpdateUserAsync(int id, UserUpdateDTO dto, string token, CancellationToken ct = default);
        Task UpdateUserPasswordAsync(int id, UserPasswordUpdateDTO dto, string token, CancellationToken ct = default);
        Task UpdateUserRoleAsync(int id, UserRoleUpdateDTO dto, string token, CancellationToken ct = default);
    }
}