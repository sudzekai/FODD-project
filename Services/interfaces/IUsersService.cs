using Shared.dtos.users;
using Shared.requests;

namespace Services.interfaces
{
    public interface IUsersService
    {
        Task<int> CreateUserAsync(UserCreateDTO dto);
        Task DeleteUserByIdAsync(int id);
        Task<UserDTO> GetUserByIdAsync(int id);
        Task<List<UserSimpleDTO>> GetUsersAsync(GetListRequest req);
        Task<int> GetUsersCountAsync();
        Task UpdateUserByIdAsync(int id, UserUpdateDTO dto);
        Task UpdateUserPasswordByIdAsync(int id, UserPasswordUpdateDTO dto);
        Task UpdateUserRoleByIdAsync(int id, UserRoleUpdateDTO dto);
    }
}