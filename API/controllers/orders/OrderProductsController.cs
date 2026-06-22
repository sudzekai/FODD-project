using API.helpers;
using API.requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.orders.services;
using Shared.dtos.orders;
using System.ComponentModel.DataAnnotations;

namespace API.controllers.orders
{
    [ApiController]
    [Route("orders")]
    public class OrderProductsController : ControllerBase
    {
        private readonly IOrderProductsService _svc;

        public OrderProductsController(IOrderProductsService svc)
        {
            _svc = svc;
        }

        [HttpGet("{id}/products")]
        [Authorize(Roles = "Клиент,Менеджер,Администратор")]
        public async Task<IActionResult> GetOrderProductsByOrderId([FromRoute] ByIdRequest req)
             => await RequestExecutor.Execute(async () =>
             {
                 var data = await _svc.GetOrderProductsByOrderIdAsync(req.Id);
                 return ResponseBuilder.BuildOk(data);
             }, ModelState);

        [HttpGet("{id}/products/count")]
        [Authorize(Roles = "Клиент,Менеджер,Администратор")]
        public async Task<IActionResult> GetOrderProductsCountByOrderId([FromRoute] ByIdRequest req)
             => await RequestExecutor.Execute(async () =>
             {
                 var data = await _svc.GetOrderProductsCountByOrderIdAsync(req.Id);
                 return ResponseBuilder.BuildOk(data);
             }, ModelState);

        [HttpGet("{id}/products/sum")]
        [Authorize(Roles = "Клиент,Менеджер,Администратор")]
        public async Task<IActionResult> GetOrderProductsSumCountByOrderId([FromRoute] ByIdRequest req)
             => await RequestExecutor.Execute(async () =>
             {
                 var data = await _svc.GetOrderProductsSumCountByOrderIdAsync(req.Id);
                 return ResponseBuilder.BuildOk(data);
             }, ModelState);

        [HttpPost("{id}/products/add")]
        [Authorize(Roles = "Клиент,Менеджер,Администратор")]
        public async Task<IActionResult> AddOrderProductByOrderId(
            [FromRoute] ByIdRequest req,
            [Required(ErrorMessage = "Товар обязателен")]
            [FromBody] OrderProductUpdateDTO dto)
             => await RequestExecutor.Execute(async () =>
             {
                 await _svc.AddOrderProductByOrderIdAsync(req.Id, dto);
                 return ResponseBuilder.BuildOk<object?>(null);
             }, ModelState);

        [HttpPost("{id}/products/remove")]
        [Authorize(Roles = "Клиент,Менеджер,Администратор")]
        public async Task<IActionResult> RemoveOrderProductByOrderIdAsync(
            [FromRoute] ByIdRequest req,
            [Required(ErrorMessage = "Товар обязателен")]
            [FromBody] OrderProductUpdateDTO dto)
             => await RequestExecutor.Execute(async () =>
             {
                 await _svc.RemoveOrderProductByOrderIdAsync(req.Id, dto);
                 return ResponseBuilder.BuildOk<object?>(null);
             }, ModelState);

        [HttpPost("{id}/products/delete")]
        [Authorize(Roles = "Клиент,Менеджер,Администратор")]
        public async Task<IActionResult> DeleteOrderProductByOrderId(
            [FromRoute] ByIdRequest req,
            [Required(ErrorMessage = "Товар обязателен")]
            [FromBody] OrderProductUpdateDTO dto)
             => await RequestExecutor.Execute(async () =>
             {
                 await _svc.DeleteOrderProductByOrderIdAsync(req.Id, dto);
                 return ResponseBuilder.BuildOk<object?>(null);
             }, ModelState);
    }
}
