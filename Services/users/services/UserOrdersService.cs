using DB.dbContext;
using DB.models;
using Microsoft.EntityFrameworkCore;
using Services.unitOfWork;
using Services.users.interfaces;
using Shared.dtos.orders;
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

        public async Task<List<OrderSimpleDTO>> GetUserOrdersByUserIdAsync(int id)
        {
            var entry = await _users.Include(u => u.Orders).FirstOrDefaultAsync(u => u.Id == id)
                ?? throw new NotFoundException("Пользователь с таким id не найден");

            return [.. entry.Orders.Select(o =>
                new OrderSimpleDTO(
                    o.Id,
                    o.CreationDateTime,
                    o.DeliveryDate,
                    o.ReceiptCode,
                    o.StatusId,
                    o.UserId)
                )];
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
    }
}
