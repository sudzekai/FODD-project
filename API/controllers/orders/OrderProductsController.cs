using API.helpers;
using API.requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.orders.interfaces;
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

        [HttpPost("user/{id}/products/add")]
        [Authorize(Roles = "Клиент,Менеджер,Администратор")]
        public async Task<IActionResult> AddOrderProductByUserId(
            [FromRoute] ByIdRequest req,
            [Required(ErrorMessage = "Товар обязателен")]
            [FromBody] OrderProductUpdateDTO dto)
             => await RequestExecutor.Execute(async () =>
             {
                 await _svc.AddOrderProductByUserIdAsync(req.Id, dto);
                 return ResponseBuilder.BuildOk<object?>(null);
             }, ModelState);

        [HttpPost("user/{id}/products/remove")]
        [Authorize(Roles = "Клиент,Менеджер,Администратор")]
        public async Task<IActionResult> RemoveOrderProductByUserIdAsync(
            [FromRoute] ByIdRequest req,
            [Required(ErrorMessage = "Товар обязателен")]
            [FromBody] OrderProductUpdateDTO dto)
             => await RequestExecutor.Execute(async () =>
             {
                 await _svc.RemoveOrderProductByUserIdAsync(req.Id, dto);
                 return ResponseBuilder.BuildOk<object?>(null);
             }, ModelState);

        [HttpPost("user/{id}/products/delete")]
        [Authorize(Roles = "Клиент,Менеджер,Администратор")]
        public async Task<IActionResult> DeleteOrderProductByUserId(
            [FromRoute] ByIdRequest req,
            [Required(ErrorMessage = "Товар обязателен")]
            [FromBody] OrderProductUpdateDTO dto)
             => await RequestExecutor.Execute(async () =>
             {
                 await _svc.DeleteOrderProductByUserIdAsync(req.Id, dto);
                 return ResponseBuilder.BuildOk<object?>(null);
             }, ModelState);
    }
}
