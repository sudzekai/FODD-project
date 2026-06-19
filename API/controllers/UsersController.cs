using API.helpers;
using Microsoft.AspNetCore.Mvc;
using Services.users.interfaces;
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
                GetListRequest parameters = new(
                    request.Offset,
                    request.Limit,
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
        public async Task<IActionResult> GetUserById(
            [FromRoute]
            [Required(ErrorMessage = "Id обязателен")]
            [Range(1, int.MaxValue, ErrorMessage = "Id должен быть больше 0")]
            int id)
        {
            if (RequestValidator.ValidateModel(ModelState) is var errors && errors.Count > 0)
                return ResponseBuilder.BuildBadRequestErrors(errors);

            try
            {
                var data = await _svc.GetUserByIdAsync(id);

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
            [FromRoute]
            [Required(ErrorMessage = "Id обязателен")]
            [Range(1, int.MaxValue, ErrorMessage = "Id должен быть больше 0")]
            int id,
            [Required(ErrorMessage = "Данные пользователя (login, password, fullName) обязательны")]
            [FromBody] UserUpdateDTO user)
        {
            if (RequestValidator.ValidateModel(ModelState) is var errors && errors.Count > 0)
                return ResponseBuilder.BuildBadRequestErrors(errors);

            try
            {
                await _svc.UpdateUserByIdAsync(id, user);

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
            [FromRoute]
            [Required(ErrorMessage = "Id обязателен")]
            [Range(1, int.MaxValue, ErrorMessage = "Id должен быть больше 0")]
            int id,
            [Required(ErrorMessage = "Данные пользователя (password) обязательны")]
            [FromBody] UserPasswordUpdateDTO user)
        {
            if (RequestValidator.ValidateModel(ModelState) is var errors && errors.Count > 0)
                return ResponseBuilder.BuildBadRequestErrors(errors);

            try
            {
                await _svc.UpdateUserPasswordByIdAsync(id, user);

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
            [FromRoute]
            [Required(ErrorMessage = "Id обязателен")]
            [Range(1, int.MaxValue, ErrorMessage = "Id должен быть больше 0")]
            int id,
            [Required(ErrorMessage = "Данные пользователя (roleId) обязательны")]
            [FromBody] UserPasswordUpdateDTO user)
        {
            if (RequestValidator.ValidateModel(ModelState) is var errors && errors.Count > 0)
                return ResponseBuilder.BuildBadRequestErrors(errors);

            try
            {
                await _svc.UpdateUserPasswordByIdAsync(id, user);

                return ResponseBuilder.BuildOk<object?>(null);
            }
            catch (Exception ex)
            {
                var error = ExceptionHandler.Handle(ex);
                return ResponseBuilder.BuildError(error);
            }
        }

        [HttpDelete("{id}", Name = "DeleteUserById")]
        public async Task<IActionResult> DeleteUserById(
            [FromRoute]
            [Required(ErrorMessage = "Id обязателен")]
            [Range(1, int.MaxValue, ErrorMessage = "Id должен быть больше 0")]
            int id)
        {
            if (RequestValidator.ValidateModel(ModelState) is var errors && errors.Count > 0)
                return ResponseBuilder.BuildBadRequestErrors(errors);

            try
            {
                await _svc.DeleteUserByIdAsync(id);

                return ResponseBuilder.BuildOk<object?>(null);
            }
            catch (Exception ex)
            {
                var error = ExceptionHandler.Handle(ex);
                return ResponseBuilder.BuildError(error);
            }
        }

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
