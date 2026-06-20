using API.helpers;
using API.requests;
using DB.models;
using Microsoft.AspNetCore.Mvc;
using Services.users.interfaces;
using Shared.dtos.users;
using Shared.requests;
using System.ComponentModel.DataAnnotations;

namespace API.controllers.users
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _svc;

        public UsersController(IUsersService svc)
        {
            _svc = svc;
        }

        [HttpGet(Name = "GetUsers")]
        public async Task<IActionResult> GetUsers([FromQuery] GetListRequest request)
            => await RequestExecutor.Execute(async () =>
            {
                var data = await _svc.GetUsersAsync(request);

                return ResponseBuilder.BuildOk(data);
            }, ModelState);

        [HttpGet(template: "{id}", Name = "GetUserById")]
        public async Task<IActionResult> GetUserById([FromRoute] ByIdRequest req)
            => await RequestExecutor.Execute(async () =>
            {
                var data = await _svc.GetUserByIdAsync(req.Id);

                return ResponseBuilder.BuildOk(data);
            }, ModelState);

        [HttpPost(Name = "PostUser")]
        public async Task<IActionResult> PostUser(
            [Required(ErrorMessage = "Данные пользователя (login, password, fullName) обязательны")]
            [FromBody] UserCreateDTO user)
            => await RequestExecutor.Execute(async () =>
            {
                var id = await _svc.CreateUserAsync(user);
                var data = await _svc.GetUserByIdAsync(id);

                return ResponseBuilder.BuildCreated(data);
            }, ModelState);

        [HttpPut("{id}", Name = "PutUserById")]
        public async Task<IActionResult> PutUserById(
            [FromRoute] ByIdRequest req,
            [Required(ErrorMessage = "Данные пользователя (login, password, fullName) обязательны")]
            [FromBody] UserUpdateDTO user)
            => await RequestExecutor.Execute(async () =>
            {
                await _svc.UpdateUserByIdAsync(req.Id, user);

                return ResponseBuilder.BuildOk<object?>(null);
            }, ModelState);

        [HttpPut("{id}/password", Name = "PutUserPasswordById")]
        public async Task<IActionResult> PutUserPasswordById(
            [FromRoute] ByIdRequest req,
            [Required(ErrorMessage = "Данные пользователя (password) обязательны")]
            [FromBody] UserPasswordUpdateDTO user)
            => await RequestExecutor.Execute(async () =>
            {
                await _svc.UpdateUserPasswordByIdAsync(req.Id, user);

                return ResponseBuilder.BuildOk<object?>(null);
            }, ModelState);

        [HttpPut("{id}/role", Name = "PutUserRoleById")]
        public async Task<IActionResult> PutUserRoleById(
            [FromRoute] ByIdRequest req,
            [Required(ErrorMessage = "Данные пользователя (roleId) обязательны")]
            [FromBody] UserRoleUpdateDTO user)
            => await RequestExecutor.Execute(async () =>
            {
                await _svc.UpdateUserRoleByIdAsync(req.Id, user);

                return ResponseBuilder.BuildOk<object?>(null);
            }, ModelState);

        [HttpDelete("{id}", Name = "DeleteUserById")]
        public async Task<IActionResult> DeleteUserById([FromRoute] ByIdRequest req)
            => await RequestExecutor.Execute(async () =>
            {
                await _svc.DeleteUserByIdAsync(req.Id);

                return ResponseBuilder.BuildOk<object?>(null);
            }, ModelState);

        [HttpPut("count", Name = "GetUsersCount")]
        public async Task<IActionResult> GetCount()
            => await RequestExecutor.Execute(async () =>
            {
                var data = await _svc.GetUsersCountAsync();

                return ResponseBuilder.BuildOk(data);
            }, ModelState);
    }
}
