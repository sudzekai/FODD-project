using DB.dbContext;
using DB.models;
using Microsoft.EntityFrameworkCore;
using Services.roles.interfaces;
using Services.unitOfWork;
using Shared.dtos.roles;
using Shared.requests;
using Shared.types.exceptions;

namespace Services.roles.services
{
    public class RolesService : IRolesService
    {
        private readonly DbSet<Role> _roles;
        private readonly IUnitOfWork _uow;

        public RolesService(ShoesStoreDbContext ctx, IUnitOfWork uow)
        {
            _roles = ctx.Roles;
            _uow = uow;
        }

        public async Task<List<RoleDTO>> GetRolesAsync(GetListRequest req)
        {
            var query = _roles.AsQueryable();

            if (!string.IsNullOrWhiteSpace(req.SearchTerm))
            {
                query = query.Where(
                    u => u.Name.Contains(req.SearchTerm)
                );
            }

            query = req.OrderDirection switch
            {
                "asc" => query.OrderBy(r => r.Id),
                _ => query.OrderByDescending(r => r.Id)

            };

            query = query.Skip(req.Offset)
                         .Take(req.Limit);

            var result = await query.Select(
                r => new RoleDTO(r.Id, r.Name)
            ).ToListAsync();

            return result;
        }

        public async Task<RoleDTO> GetRoleByIdAsync(int id)
        {
            var query = _roles.AsQueryable();

            var entry = await query.FirstOrDefaultAsync(u => u.Id == id)
                ?? throw new NotFoundException("Роль с таким id не найдена");

            var result = new RoleDTO(
                entry.Id,
                entry.Name
            );

            return result;
        }
    }
}
