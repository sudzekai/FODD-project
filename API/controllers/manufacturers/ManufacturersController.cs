using API.helpers;
using API.requests;
using Microsoft.AspNetCore.Mvc;
using Services.manufacturers.interfaces;
using Shared.dtos.manufacturers;
using Shared.requests;
using System.ComponentModel.DataAnnotations;

namespace API.controllers.Manufacturers
{
    [ApiController]
    [Route("[controller]")]
    public class ManufacturersController : ControllerBase
    {
        private readonly IManufacturersService _svc;

        public ManufacturersController(IManufacturersService svc)
        {
            _svc = svc;
        }

        [HttpGet]
        public async Task<IActionResult> GetManufacturers([FromQuery] GetListRequest req)
            => await RequestExecutor.Execute(async () =>
            {
                var data = await _svc.GetManufacturersAsync(req);
                return ResponseBuilder.BuildOk(data);
            }, ModelState);

        [HttpGet("{id}")]
        public async Task<IActionResult> GetManufacturerById([FromRoute] ByIdRequest req)
            => await RequestExecutor.Execute(async () =>
            {
                var data = await _svc.GetManufacturerByIdAsync(req.Id);
                return ResponseBuilder.BuildOk(data);
            }, ModelState);

        [HttpPost()]
        public async Task<IActionResult> PostManufacturer(
            [Required(ErrorMessage = "Информация о производителе обязательна")]
            [FromBody] ManufacturerWriteDTO dto)
            => await RequestExecutor.Execute(async () =>
            {
                var id = await _svc.CreateManufacturerAsync(dto);
                var data = await _svc.GetManufacturerByIdAsync(id);
                return ResponseBuilder.BuildCreated(data);
            }, ModelState);

        [HttpPut("{id}")]
        public async Task<IActionResult> PutManufacturer(
            [FromRoute] ByIdRequest req,
            [Required(ErrorMessage = "Информация о производителе обязательна")]
            [FromBody] ManufacturerWriteDTO dto)
            => await RequestExecutor.Execute(async () =>
            {
                await _svc.UpdateManufacturerByIdAsync(req.Id, dto);
                return ResponseBuilder.BuildOk<object?>(null);
            }, ModelState);

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteManufacturerById([FromRoute] ByIdRequest req)
            => await RequestExecutor.Execute(async () =>
            {
                await _svc.DeleteManufacturerByIdAsync(req.Id);
                return ResponseBuilder.BuildOk<object?>(null);
            }, ModelState);

        [HttpGet("count")]
        public async Task<IActionResult> GetManufacturersCount()
            => await RequestExecutor.Execute(async () =>
            {
                var data = await _svc.GetManufacturersCountAsync();
                return ResponseBuilder.BuildOk(data);
            }, ModelState);
    }
}
