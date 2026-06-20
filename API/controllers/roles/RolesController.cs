using API.helpers;
using API.requests;
using Microsoft.AspNetCore.Mvc;
using Services.roles.interfaces;
using Shared.requests;

namespace API.controllers.roles
{
    [ApiController]
    [Route("[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly IRolesService _svc;

        public RolesController(IRolesService svc)
        {
            _svc = svc;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles([FromQuery] GetListRequest req)
            => await RequestExecutor.Execute(async () =>
            {
                var data = await _svc.GetRolesAsync(req);
                return ResponseBuilder.BuildOk(data);
            }, ModelState);

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById([FromRoute] ByIdRequest req)
            => await RequestExecutor.Execute(async () =>
            {
                var data = await _svc.GetRoleByIdAsync(req.Id);
                return ResponseBuilder.BuildOk(data);
            }, ModelState);

        [HttpGet("count")]
        public async Task<IActionResult> GetRolesCount()
            => await RequestExecutor.Execute(async () =>
            {
                var data = await _svc.GetRolesCountAsync();
                return ResponseBuilder.BuildOk(data);
            }, ModelState);
    }
}
