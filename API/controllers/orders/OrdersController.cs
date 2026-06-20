using API.helpers;
using API.requests;
using Microsoft.AspNetCore.Mvc;
using Services.orders.interfaces;
using Shared.dtos.orders;
using Shared.dtos.products;
using Shared.requests;
using System.ComponentModel.DataAnnotations;

namespace API.controllers.orders
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _svc;

        public OrdersController(IOrdersService svc)
        {
            _svc = svc;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromRoute] GetListRequest req)
            => await RequestExecutor.Execute(async () =>
            {
                var data = await _svc.GetOrdersAsync(req);
                return ResponseBuilder.BuildOk(data);
            }, ModelState);

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById([FromRoute] ByIdRequest req)
            => await RequestExecutor.Execute(async () =>
            {
                var data = await _svc.GetOrderByIdAsync(req.Id);
                return ResponseBuilder.BuildOk(data);
            }, ModelState);

        [HttpPut("{id}/status")]
        public async Task<IActionResult> PutProduct(
            [FromRoute] ByIdRequest req,
            [Required(ErrorMessage = "Информация о статусе обязательна")]
            [FromBody] OrderStatusUpdateDTO dto)
            => await RequestExecutor.Execute(async () =>
            {
                await _svc.UpdateOrderStatusByOrderIdAsync(req.Id, dto);
                return ResponseBuilder.BuildOk<object?>(null);
            }, ModelState);

        [HttpPut("{id}/delivery")]
        public async Task<IActionResult> PutProductPricing(
            [FromRoute] ByIdRequest req,
            [Required(ErrorMessage = "Информация о доставке товара обязательна")]
            [FromBody] OrderDeliveryUpdateDTO dto)
            => await RequestExecutor.Execute(async () =>
            {
                await _svc.UpdateOrderDeliveryByOrderIdAsync(req.Id, dto);
                return ResponseBuilder.BuildOk<object?>(null);
            }, ModelState);

        [HttpDelete("{id}")]
        public async Task<IActionResult> PutProductRelations([FromRoute] ByIdRequest req)
            => await RequestExecutor.Execute(async () =>
            {
                await _svc.DeleteOrderByIdAsync(req.Id);
                return ResponseBuilder.BuildOk<object?>(null);
            }, ModelState);

        [HttpGet("count")]
        public async Task<IActionResult> GetOrdersCount()
            => await RequestExecutor.Execute(async () =>
            {
                var data = await _svc.GetOrdersCountAsync();
                return ResponseBuilder.BuildOk(data);
            }, ModelState);
    }
}
