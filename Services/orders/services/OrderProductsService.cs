using DB.dbContext;
using DB.models;
using Microsoft.EntityFrameworkCore;
using Services.unitOfWork;
using Shared.dtos.orders;
using Shared.types.exceptions;

namespace Services.orders.services
{
    public class OrderProductsService
    {
        private readonly DbSet<Order> _orders;
        private readonly DbSet<Product> _products;
        private readonly DbSet<OrderProduct> _orderProducts;

        private readonly IUnitOfWork _uow;

        public OrderProductsService(ShoesStoreDbContext ctx, IUnitOfWork uow)
        {
            _orderProducts = ctx.OrderProducts;
            _orders = ctx.Orders;
            _products = ctx.Products;
            _uow = uow;
        }

        public async Task<List<OrderProductDTO>> GetOrderProductsByOrderIdAsync(int orderId)
        {
            var result = await _orders
                .Where(o => o.Id == orderId)
                .SelectMany(o => o.OrderProducts)
                .Join(_products,
                    op => op.ProductId,
                    p => p.Id,
                    (op, p) => new OrderProductDTO(
                        p.Id,
                        p.Article,
                        p.Name,
                        p.Price,
                        p.Discount,
                        op.Quantity
                    ))
                .ToListAsync();

            if (result.Count == 0)
                throw new NotFoundException("Заказ с таким id не найден");

            return result;
        }

        public async Task AddOrderProductByOrderIdAsync(int orderId, OrderProductUpdateDTO dto)
        {
            var order = await _orders
                .FirstOrDefaultAsync(o => o.Id == orderId)
                ?? throw new NotFoundException("Заказ с таким id не найден");

            var product = await _products
                .FirstOrDefaultAsync(p => p.Id == dto.ProductId)
                ?? throw new NotFoundException("Товар с таким id не найден");

            var existing = await _orderProducts
                .FirstOrDefaultAsync(op => op.OrderId == orderId && op.ProductId == dto.ProductId);

            if (existing != null)
            {
                existing.Quantity += 1;
            }
            else
            {
                var orderProduct = new OrderProduct
                {
                    OrderId = orderId,
                    ProductId = dto.ProductId,
                    Quantity = 1
                };

                await _orderProducts.AddAsync(orderProduct);
            }

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch
            {
                throw new InternalServerException("Возникла ошибка при добавлении товара в заказ");
            }
        }

        public async Task RemoveOrderProductByOrderIdAsync(int orderId, OrderProductUpdateDTO dto)
        {
            var existing = await _orderProducts
                .FirstOrDefaultAsync(op => op.OrderId == orderId && op.ProductId == dto.ProductId)
                ?? throw new NotFoundException("Такого товара в заказе нет");

            if (existing.Quantity == 1)
                _orderProducts.Remove(existing);
            else
                existing.Quantity--;

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch
            {
                throw new InternalServerException("Возникла ошибка при уменьшении количества товара в заказе");
            }
        }

        public async Task DeleteOrderProductByOrderIdAsync(int orderId, OrderProductUpdateDTO dto)
        {
            var existing = await _orderProducts
                .FirstOrDefaultAsync(op => op.OrderId == orderId && op.ProductId == dto.ProductId)
                ?? throw new NotFoundException("Такого товара в заказе нет");

            _orderProducts.Remove(existing);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch
            {
                throw new InternalServerException("Возникла ошибка при уменьшении количества товара в заказе");
            }
        }
    }
}
