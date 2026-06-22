using Shared.dtos.users;
using Shared.requests;

namespace Services.auth.interfaces
{
    public interface IAuthService
    {
        Task<UserDTO> GetUserByAuthRequestAsync(AuthRequest req);
    }
}