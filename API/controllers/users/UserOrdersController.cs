using API.helpers;
using API.requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.users.interfaces;

namespace API.controllers.users
{
    [ApiController]
    [Route("users")]
    public class UserOrdersController : ControllerBase
    {
        private readonly IUserOrdersService _userOrdersSvc;

        public UserOrdersController(IUserOrdersService userOrdersSvc)
        {
            _userOrdersSvc = userOrdersSvc;
        }

        [HttpGet("{id}/orders")]
        [Authorize(Roles = "Клиент,Менеджер,Администратор")]
        public async Task<IActionResult> GetUserOrdersByUserId([FromRoute] ByIdRequest req)
            => await RequestExecutor.Execute(async () =>
            {
                var data = await _userOrdersSvc.GetUserOrdersByUserIdAsync(req.Id);

                return ResponseBuilder.BuildOk(data);
            }, ModelState);

        [HttpGet("{id}/orders/count")]
        [Authorize(Roles = "Клиент,Менеджер,Администратор")]
        public async Task<IActionResult> GetUserOrdersCountByUserId([FromRoute] ByIdRequest req)
            => await RequestExecutor.Execute(async () =>
            {
                var data = await _userOrdersSvc.GetUserOrdersCountByUserIdAsync(req.Id);

                return ResponseBuilder.BuildOk(data);
            }, ModelState);

        [HttpPost("{id}/orders")]
        [Authorize(Roles = "Клиент,Менеджер,Администратор")]
        public async Task<IActionResult> PostUserOrderByUserId([FromRoute] ByIdRequest req)
            => await RequestExecutor.Execute(async () =>
            {
                var data = await _userOrdersSvc.CreateOrderByUserId(req.Id);

                return ResponseBuilder.BuildOk(data);
            }, ModelState);
    }
}
