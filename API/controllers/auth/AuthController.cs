using API.helpers;
using API.utilities;
using Microsoft.AspNetCore.Mvc;
using Services.auth.interfaces;
using Services.roles.interfaces;
using Shared.requests;
using Shared.responses;
using System.ComponentModel.DataAnnotations;

namespace API.controllers.auth
{
    [ApiController]
    [Route("/login")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;
        private readonly IRolesService _rolesService;
        private readonly IJwtTokenService _tokenService;

        public AuthController(IAuthService service, IRolesService rolesService, IJwtTokenService tokenService)
        {
            _service = service;
            _rolesService = rolesService;
            _tokenService = tokenService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(
            [FromBody]
            [Required(ErrorMessage = "Логин и пароль обязательны")]
            AuthRequest req)
            => await RequestExecutor.Execute(async () =>
            {
                var user = await _service.GetUserByAuthRequestAsync(req);

                var role = await _rolesService.GetRoleByIdAsync(user.RoleId);

                var token = _tokenService.GenerateToken(user.Id.ToString(), role.Name, user.FullName);

                return ResponseBuilder.BuildOk(new AuthResponse(role.Name, token, user.FullName));
            }, ModelState);
    }
}
