using DB.dbContext;
using DB.models;
using Microsoft.EntityFrameworkCore;
using Services.auth.interfaces;
using Services.utilities;
using Shared.dtos.users;
using Shared.requests;
using Shared.types.exceptions;

namespace Services.auth.services
{
    public class AuthService : IAuthService
    {
        private readonly DbSet<User> _users;

        public AuthService(ShoesStoreDbContext ctx)
        {
            _users = ctx.Users;
        }

        public async Task<UserDTO> GetUserByAuthRequestAsync(AuthRequest req)
        {
            var entry = await _users.FirstOrDefaultAsync(u => u.Login == req.Login)
                ?? throw new NotFoundException("Неверный логин или пароль");

            if (!HashService.Compare(entry.Password, req.Password))
                throw new NotFoundException("Неверный логин или пароль");

            return new(entry.Id, entry.Login, entry.RoleId, entry.FullName);
        }
    }
}
