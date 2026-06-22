using DB.dbContext;
using DB.models;
using Microsoft.EntityFrameworkCore;
using Services.orders.interfaces;
using Services.unitOfWork;
using Shared.dtos.orders;
using Shared.requests;
using Shared.types.exceptions;

namespace Services.orders.services
{
    public class OrdersService : IOrdersService
    {
        private readonly DbSet<Order> _orders;
        private readonly IUnitOfWork _uow;

        public OrdersService(ShoesStoreDbContext ctx, IUnitOfWork uow)
        {
            _orders = ctx.Orders;
            _uow = uow;
        }

        public async Task<List<OrderSimpleDTO>> GetOrdersAsync(GetOrdersListRequest req)
        {
            var query = _orders.AsQueryable();

            if (!string.IsNullOrWhiteSpace(req.SearchTerm))
            {
                query = query.Include(o => o.User).Where(o =>
                        o.Id.ToString().Equals(req.SearchTerm)
                        || o.User.FullName.Contains(req.SearchTerm)
                        || o.User.Login.Contains(req.SearchTerm)
                    );
            }

            if (req.OrderDirection == "asc")
            {
                if (req.OrderBy == "creationDate")
                    query = query.OrderBy(p => p.CreationDateTime);
                else if (req.OrderBy == "deliveryDate")
                    query = query.OrderBy(p => p.DeliveryDate);
                else
                    query = query.OrderBy(p => p.Id);
            }
            else
            {
                if (req.OrderBy == "creationDate")
                    query = query.OrderByDescending(p => p.CreationDateTime);
                else if (req.OrderBy == "deliveryDate")
                    query = query.OrderByDescending(p => p.DeliveryDate);
                else
                    query = query.OrderByDescending(p => p.Id);
            }

            if (req.StatusId != 0)
                query = query.Where(p => p.StatusId == req.StatusId);

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

            query = query.Skip(req.Offset)
                         .Take(req.Limit);

            var result = await query.Select(
                p => new OrderSimpleDTO(p.Id, p.CreationDateTime, p.DeliveryDate, p.ReceiptCode, p.StatusId, p.UserId)
            ).ToListAsync();

            return result;
        }

        public async Task<OrderDTO> GetOrderByIdAsync(int id)
        {
            var entry = await _orders.Include(o => o.OrderProducts).FirstOrDefaultAsync(u => u.Id == id)
                ?? throw new NotFoundException("Заказ с таким id не найден");

            var result = new OrderDTO(
                entry.Id,
                entry.CreationDateTime,
                entry.DeliveryDate,
                entry.ReceiptCode,
                entry.StatusId,
                [.. entry.OrderProducts.Select(p => p.ProductId)],
                entry.UserId
            );

            return result;
        }

        public async Task UpdateOrderStatusByOrderIdAsync(int id, OrderStatusUpdateDTO dto)
        {
            var entry = await _orders.FirstOrDefaultAsync(u => u.Id == id)
                ?? throw new NotFoundException("Заказ с таким id не найден");

            if (entry.StatusId == dto.StatusId)
                throw new ConflictException("Исходные данные совпадают с переданными");

            entry.StatusId = dto.StatusId;

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch
            {
                throw new InternalServerException("Возникла ошибка при обновлении статуса заказа");
            }
        }

        public async Task UpdateOrderDeliveryByOrderIdAsync(int id, OrderDeliveryUpdateDTO dto)
        {
            var entry = await _orders.FirstOrDefaultAsync(u => u.Id == id)
                ?? throw new NotFoundException("Заказ с таким id не найден");

            if (entry.DeliveryDate == dto.DeliveryDate
                || entry.ReceiptCode == dto.ReceiptCode)
                throw new ConflictException("Исходные данные совпадают с переданными");

            entry.DeliveryDate = dto.DeliveryDate;
            entry.ReceiptCode = dto.ReceiptCode;

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch
            {
                throw new InternalServerException("Возникла ошибка при обновлении заказа");
            }
        }

        public async Task DeleteOrderByIdAsync(int id)
        {
            var entry = await _orders.FirstOrDefaultAsync(u => u.Id == id)
                ?? throw new NotFoundException("Заказ с таким id не найден");

            _orders.Remove(entry);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch
            {
                throw new InternalServerException("Возникла ошибка при удалении заказа");
            }
        }

        public async Task<int> GetOrdersCountAsync() => await _orders.CountAsync();
    }
}
