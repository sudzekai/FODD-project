using API.helpers;
using API.requests;
using Microsoft.AspNetCore.Mvc;
using Services.statuses.interfaces;
using Shared.dtos.statuses;
using Shared.requests;
using System.ComponentModel.DataAnnotations;

namespace API.controllers.statuses
{
    [ApiController]
    [Route("[controller]")]
    public class StatusesController : ControllerBase
    {
        private readonly IStatusesService _svc;

        public StatusesController(IStatusesService svc)
        {
            _svc = svc;
        }

        [HttpGet]
        public async Task<IActionResult> GetStatuses()
            => await RequestExecutor.Execute(async () =>
            {
                var data = await _svc.GetStatusesAsync();
                return ResponseBuilder.BuildOk(data);
            }, ModelState);

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStatusById([FromRoute] ByIdRequest req)
            => await RequestExecutor.Execute(async () =>
            {
                var data = await _svc.GetStatusByIdAsync(req.Id);
                return ResponseBuilder.BuildOk(data);
            }, ModelState);

        [HttpPost()]
        public async Task<IActionResult> PostStatus(
            [Required(ErrorMessage = "Информация о статусе обязательна")]
            [FromBody] StatusWriteDTO dto)
            => await RequestExecutor.Execute(async () =>
            {
                var id = await _svc.CreateStatusAsync(dto);
                var data = await _svc.GetStatusByIdAsync(id);
                return ResponseBuilder.BuildCreated(data);
            }, ModelState);

        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatus(
            [FromRoute] ByIdRequest req,
            [Required(ErrorMessage = "Информация о статусе обязательна")]
            [FromBody] StatusWriteDTO dto)
            => await RequestExecutor.Execute(async () =>
            {
                await _svc.UpdateStatusByIdAsync(req.Id, dto);
                return ResponseBuilder.BuildOk<object?>(null);
            }, ModelState);

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatusById([FromRoute] ByIdRequest req)
            => await RequestExecutor.Execute(async () =>
            {
                await _svc.DeleteStatusByIdAsync(req.Id);
                return ResponseBuilder.BuildOk<object?>(null);
            }, ModelState);

        [HttpGet("count")]
        public async Task<IActionResult> GetStatusesCount()
            => await RequestExecutor.Execute(async () =>
            {
                var data = await _svc.GetStatusesCountAsync();
                return ResponseBuilder.BuildOk(data);
            }, ModelState);
    }
}
