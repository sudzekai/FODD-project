using DB.dbContext;
using DB.models;
using Microsoft.EntityFrameworkCore;
using Services.unitOfWork;
using Services.users.interfaces;
using Services.utilities;
using Shared.dtos.users;
using Shared.requests;
using Shared.types.exceptions;

namespace Services.users.services
{
    public class UsersService : IUsersService
    {
        private readonly DbSet<User> _users;
        private readonly IUnitOfWork _uow;
        public UsersService(ShoesStoreDbContext ctx, IUnitOfWork uow)
        {
            _users = ctx.Users;
            _uow = uow;
        }

        public async Task<List<UserSimpleDTO>> GetUsersAsync(GetListRequest req)
        {
            var query = _users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(req.SearchTerm))
            {
                query = query.Where(
                    u => u.Login.Contains(req.SearchTerm)
                         || u.FullName.Contains(req.SearchTerm)
                );
            }

            query = req.OrderDirection switch
            {
               "asc" => query.OrderBy(u => u.Id),
                _ => query.OrderByDescending(u => u.Id)

            };

            query = query.Skip(req.Offset)
                         .Take(req.Limit);

            var result = await query.Select(
                u => new UserSimpleDTO(u.Id, u.Login)
            ).ToListAsync();

            return result;
        }

        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            var query = _users.AsQueryable();

            var entry = await query.FirstOrDefaultAsync(u => u.Id == id)
                ?? throw new NotFoundException("Пользователь с таким id не найден");

            var result = new UserDTO(
                entry.Id,
                entry.Login,
                entry.RoleId,
                entry.FullName
            );

            return result;
        }

        public async Task<int> CreateUserAsync(UserCreateDTO dto)
        {
            var query = _users.AsQueryable();

            if (await IsLoginExistsAsync(dto.Login))
                throw new ConflictException("Пользователь с таким логином уже существует");

            var user = new User()
            {
                Login = dto.Login,
                Password = HashService.HashString(dto.Password),
                FullName = dto.FullName,
                RoleId = 1
            };

            var created = await _users.AddAsync(user);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new InternalServerException($"Возникла ошибка при создании пользователя");
            }

            return created.Entity.Id;
        }

        public async Task UpdateUserByIdAsync(int id, UserUpdateDTO dto)
        {
            var query = _users.AsQueryable();

            var existing = await query.FirstOrDefaultAsync(u => u.Id == id)
                ?? throw new NotFoundException("Пользователь с таким id не найден");

            if (existing.FullName == dto.FullName && existing.Login == dto.Login)
                throw new BadRequestException("Исходные данные совпадают с переданными");

            if (await IsLoginExistsAsync(dto.Login, id))
                throw new ConflictException("Пользователь с таким логином уже существует");

            existing.FullName = dto.FullName;
            existing.Login = dto.Login;

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new InternalServerException("Возникла ошибка при обновлении данных пользователя");
            }
        }

        public async Task UpdateUserPasswordByIdAsync(int id, UserPasswordUpdateDTO dto)
        {
            var query = _users.AsQueryable();

            var existing = await query.FirstOrDefaultAsync(u => u.Id == id)
                ?? throw new NotFoundException("Пользователь с таким id не найден");

            if (HashService.Compare(existing.Password, dto.Password))
                throw new ConflictException("Новый пароль не может совпадать со старым");

            var hashedPassword = HashService.HashString(dto.Password);

            existing.Password = hashedPassword;

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch
            {
                throw new InternalServerException("Возникла ошибка при изменении пароля пользователя");
            }
        }

        public async Task UpdateUserRoleByIdAsync(int id, UserRoleUpdateDTO dto)
        {
            var query = _users.AsQueryable();

            var existing = await query.FirstOrDefaultAsync(u => u.Id == id)
                ?? throw new NotFoundException("Пользователь с таким id не найден");

            if (existing.RoleId == dto.RoleId)
                throw new InvalidOperationException("Исходные данные совпадают с переданными");

            existing.RoleId = dto.RoleId;

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch
            {
                throw new InternalServerException("Возникла ошибка при изменении роли пользователя");
            }
        }

        public async Task DeleteUserByIdAsync(int id)
        {
            var query = _users.AsQueryable();

            var existing = await query.FirstOrDefaultAsync(u => u.Id == id)
                ?? throw new NotFoundException("Пользователь с таким id не найден");

            _users.Remove(existing);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch
            {
                throw new InternalServerException("Возникла ошибка при удалении пользователя");
            }
        }

        public async Task<int> GetUsersCountAsync() => await _users.CountAsync();

        private async Task<bool> IsLoginExistsAsync(string login, int? excludeUserId = null)
        {
            var query = _users.AsQueryable();

            if (excludeUserId.HasValue)
                query = query.Where(u => u.Id != excludeUserId.Value);

            return await query.AnyAsync(u => u.Login == login);
        }
    }
}