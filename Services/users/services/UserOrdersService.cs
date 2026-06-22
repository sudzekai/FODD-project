using DB.dbContext;
using DB.models;
using Microsoft.EntityFrameworkCore;
using Services.unitOfWork;
using Services.users.interfaces;
using Shared.dtos.orders;
using Shared.requests;
using Shared.types.exceptions;

namespace Services.users.services
{
    public class UserOrdersService : IUserOrdersService
    {
        private readonly DbSet<User> _users;
        private readonly DbSet<Order> _orders;

        private readonly IUnitOfWork _uow;

        public UserOrdersService(ShoesStoreDbContext ctx, IUnitOfWork uow)
        {
            _users = ctx.Users;
            _orders = ctx.Orders;
            _uow = uow;
        }

        public async Task<List<OrderSimpleDTO>> GetUserOrdersByUserIdAsync(int id, GetOrdersListRequest req)
        {
            var query = _orders
                .AsNoTracking()
                .Where(o => o.UserId == id);

            if (!string.IsNullOrWhiteSpace(req.SearchTerm))
            {
                query = query.Where(o =>
                    o.Id.ToString() == req.SearchTerm ||
                    o.User.FullName.Contains(req.SearchTerm) ||
                    o.User.Login.Contains(req.SearchTerm)
                );
            }

            if (req.StatusId != 0)
            {
                query = query.Where(p => p.StatusId == req.StatusId);
            }

            if (req.DeliveryDateTimeStart is not null)
            {
                query = query.Where(p =>
                    p.DeliveryDate.HasValue &&
                    p.DeliveryDate.Value >= req.DeliveryDateTimeStart);
            }

            if (req.DeliveryDateTimeEnd is not null)
            {
                query = query.Where(p =>
                    p.DeliveryDate.HasValue &&
                    p.DeliveryDate.Value <= req.DeliveryDateTimeEnd);
            }

            if (req.CreationDateTimeStart is not null)
            {
                query = query.Where(p =>
                    p.CreationDateTime >= req.CreationDateTimeStart);
            }

            if (req.CreationDateTimeEnd is not null)
            {
                query = query.Where(p =>
                    p.CreationDateTime <= req.CreationDateTimeEnd);
            }

            query = req.OrderDirection == "asc"
                ? req.OrderBy == "creationDate"
                    ? query.OrderBy(x => x.CreationDateTime)
                    : req.OrderBy == "deliveryDate"
                        ? query.OrderBy(x => x.DeliveryDate)
                        : query.OrderBy(x => x.Id)
                : req.OrderBy == "creationDate"
                    ? query.OrderByDescending(x => x.CreationDateTime)
                    : req.OrderBy == "deliveryDate"
                        ? query.OrderByDescending(x => x.DeliveryDate)
                        : query.OrderByDescending(x => x.Id);

            return await query
                .Skip(req.Offset)
                .Take(req.Limit)
                .Select(p => new OrderSimpleDTO(
                    p.Id,
                    p.CreationDateTime,
                    p.DeliveryDate,
                    p.ReceiptCode,
                    p.StatusId,
                    p.UserId
                ))
                .ToListAsync();
        }

        public async Task<int> CreateOrderByUserId(int id)
        {
            var user = await _users
                .FirstOrDefaultAsync(u => u.Id == id)
                ?? throw new NotFoundException("Пользователь с таким id не найден");

            var order = new Order
            {
                UserId = user.Id,
                StatusId = 1,
                CreationDateTime = DateTime.Now
            };

            await _orders.AddAsync(order);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch
            {
                throw new InternalServerException("Возникла ошибка при создании заказа для пользователя");
            }

            return order.Id;
        }

        public async Task<int> GetUserOrdersCountByUserIdAsync(int id)
        {
            var user = await _users
                .Include(u => u.Orders)
                .FirstOrDefaultAsync(u => u.Id == id)
                ?? throw new NotFoundException("Пользователь с таким id не найден");

            return user.Orders.Count();
        }


        public async Task<OrderDTO> GetCartOrderByUserIdAsync(int id)
        {
            var entry = await _users.Include(u => u.Orders).ThenInclude(o => o.OrderProducts).FirstOrDefaultAsync(u => u.Id == id)
                ?? throw new NotFoundException("Пользователь с таким id не найден");

            var order = entry.Orders.FirstOrDefault(o => o.StatusId == 1)
                ?? throw new NotFoundException("Заказ в сборке для пользователя не найден");

            return new(order.Id, order.CreationDateTime, order.DeliveryDate, order.ReceiptCode, order.StatusId, order.OrderProducts.Select(op => op.ProductId).ToList(), entry.Id);
        }
    }
}
