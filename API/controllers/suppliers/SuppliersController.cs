using API.helpers;
using API.requests;
using Microsoft.AspNetCore.Mvc;
using Services.suppliers.interfaces;
using Shared.dtos.suppliers;
using Shared.requests;
using System.ComponentModel.DataAnnotations;

namespace API.controllers.suppliers
{
    [ApiController]
    [Route("[controller]")]
    public class SuppliersController : ControllerBase
    {
        private readonly ISuppliersService _svc;

        public SuppliersController(ISuppliersService svc)
        {
            _svc = svc;
        }

        [HttpGet]
        public async Task<IActionResult> GetSuppliers([FromQuery] GetListRequest req)
            => await RequestExecutor.Execute(async () =>
            {
                var data = await _svc.GetSuppliersAsync(req);
                return ResponseBuilder.BuildOk(data);
            }, ModelState);

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSupplierById([FromRoute] ByIdRequest req)
            => await RequestExecutor.Execute(async () =>
            {
                var data = await _svc.GetSupplierByIdAsync(req.Id);
                return ResponseBuilder.BuildOk(data);
            }, ModelState);

        [HttpPost()]
        public async Task<IActionResult> PostSupplier(
            [Required(ErrorMessage = "Информация о поставщике обязательна")]
            [FromBody] SupplierWriteDTO dto)
            => await RequestExecutor.Execute(async () =>
            {
                var id = await _svc.CreateSupplierAsync(dto);
                var data = await _svc.GetSupplierByIdAsync(id);
                return ResponseBuilder.BuildCreated(data);
            }, ModelState);

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSupplier(
            [FromRoute] ByIdRequest req,
            [Required(ErrorMessage = "Информация о поставщике обязательна")]
            [FromBody] SupplierWriteDTO dto)
            => await RequestExecutor.Execute(async () =>
            {
                await _svc.UpdateSupplierByIdAsync(req.Id, dto);
                return ResponseBuilder.BuildOk<object?>(null);
            }, ModelState);

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplierById([FromRoute] ByIdRequest req)
            => await RequestExecutor.Execute(async () =>
            {
                await _svc.DeleteSupplierByIdAsync(req.Id);
                return ResponseBuilder.BuildOk<object?>(null);
            }, ModelState);

        [HttpGet("count")]
        public async Task<IActionResult> GetSuppliersCount()
            => await RequestExecutor.Execute(async () =>
            {
                var data = await _svc.GetSuppliersCountAsync();
                return ResponseBuilder.BuildOk(data);
            }, ModelState);
    }
}
