using API.helpers;
using API.requests;
using Microsoft.AspNetCore.Mvc;
using Services.interfaces;
using Shared.dtos.users;
using Shared.requests;
using System.ComponentModel.DataAnnotations;

namespace API.controllers
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
        {
            if (RequestValidator.ValidateModel(ModelState) is var errors && errors.Count > 0)
                return ResponseBuilder.BuildBadRequestErrors(errors);

            try
            {
                GetListParameters parameters = new(
                    request.GetOffset(),
                    request.GetLimit(),
                    request.SearchTerm,
                    request.OrderDirection
                );

                var data = await _svc.GetUsersAsync(parameters);

                return ResponseBuilder.BuildOk(data);
            }
            catch (Exception ex)
            {
                var error = ExceptionHandler.Handle(ex);
                return ResponseBuilder.BuildError(error);
            }
        }

        [HttpGet(template: "{id}", Name = "GetUserById")]
        public async Task<IActionResult> GetUserById([FromRoute] IdRequest request)
        {
            if (RequestValidator.ValidateModel(ModelState) is var errors && errors.Count > 0)
                return ResponseBuilder.BuildBadRequestErrors(errors);

            try
            {
                var data = await _svc.GetUserByIdAsync(request.GetId());

                return ResponseBuilder.BuildOk(data);
            }
            catch (Exception ex)
            {
                var error = ExceptionHandler.Handle(ex);
                return ResponseBuilder.BuildError(error);
            }
        }

        [HttpPost(Name = "PostUser")]
        public async Task<IActionResult> PostUser(
            [Required(ErrorMessage = "Данные пользователя (login, password, fullName) обязательны")]
            [FromBody] UserCreateDTO user)
        {
            if (RequestValidator.ValidateModel(ModelState) is var errors && errors.Count > 0)
                return ResponseBuilder.BuildBadRequestErrors(errors);

            try
            {
                var id = await _svc.CreateUserAsync(user);
                var data = await _svc.GetUserByIdAsync(id);

                return ResponseBuilder.BuildCreated(data);
            }
            catch (Exception ex)
            {
                var error = ExceptionHandler.Handle(ex);
                return ResponseBuilder.BuildError(error);
            }
        }

        [HttpPut("{id}", Name = "PutUserById")]
        public async Task<IActionResult> PutUserById(
            [FromRoute] IdRequest request,
            [Required(ErrorMessage = "Данные пользователя (login, password, fullName) обязательны")]
            [FromBody] UserUpdateDTO user)
        {
            if (RequestValidator.ValidateModel(ModelState) is var errors && errors.Count > 0)
                return ResponseBuilder.BuildBadRequestErrors(errors);

            try
            {
                await _svc.UpdateUserByIdAsync(request.GetId(), user);

                return ResponseBuilder.BuildOk<object?>(null);
            }
            catch (Exception ex)
            {
                var error = ExceptionHandler.Handle(ex);
                return ResponseBuilder.BuildError(error);
            }
        }

        [HttpPut("{id}/password", Name = "PutUserPasswordById")]
        public async Task<IActionResult> PutUserPasswordById(
            [FromRoute] IdRequest request,
            [Required(ErrorMessage = "Данные пользователя (password) обязательны")]
            [FromBody] UserPasswordUpdateDTO user)
        {
            if (RequestValidator.ValidateModel(ModelState) is var errors && errors.Count > 0)
                return ResponseBuilder.BuildBadRequestErrors(errors);

            try
            {
                await _svc.UpdateUserPasswordByIdAsync(request.GetId(), user);

                return ResponseBuilder.BuildOk<object?>(null);
            }
            catch (Exception ex)
            {
                var error = ExceptionHandler.Handle(ex);
                return ResponseBuilder.BuildError(error);
            }
        }

        [HttpPut("{id}/role", Name = "PutUserRoleById")]
        public async Task<IActionResult> PutUserRoleById(
            [FromRoute] IdRequest request,
            [Required(ErrorMessage = "Данные пользователя (roleId) обязательны")]
            [FromBody] UserPasswordUpdateDTO user)
        {
            if (RequestValidator.ValidateModel(ModelState) is var errors && errors.Count > 0)
                return ResponseBuilder.BuildBadRequestErrors(errors);

            try
            {
                await _svc.UpdateUserPasswordByIdAsync(request.GetId(), user);

                return ResponseBuilder.BuildOk<object?>(null);
            }
            catch (Exception ex)
            {
                var error = ExceptionHandler.Handle(ex);
                return ResponseBuilder.BuildError(error);
            }
        }

        [HttpDelete("{id}", Name = "DeleteUserById")]

        [HttpPut("count", Name = "GetUsersCount")]
        public async Task<IActionResult> GetCount()
        {
            try
            {
                var data = await _svc.GetUsersCountAsync();

                return ResponseBuilder.BuildOk(data);
            }
            catch (Exception ex)
            {
                var error = ExceptionHandler.Handle(ex);
                return ResponseBuilder.BuildError(error);
            }
        }
    }
}
