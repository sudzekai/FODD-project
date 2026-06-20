using Shared.dtos.users;
using Shared.requests;

namespace Clients.webClients.users.interfaces
{
    public interface IUsersWebClient
    {
        Task<UserDTO?> CreateUserAsync(UserCreateDTO dto);
        Task DeleteUserAsync(int id);
        Task<UserDTO> GetUserByIdAsync(int id);
        Task<List<UserSimpleDTO>> GetUsersAsync(GetListRequest req);
        Task<int> GetUsersCountAsync();
        Task UpdateUserAsync(int id, UserUpdateDTO dto);
        Task UpdateUserPasswordAsync(int id, UserPasswordUpdateDTO dto);
        Task UpdateUserRoleAsync(int id, UserRoleUpdateDTO dto);
    }
}