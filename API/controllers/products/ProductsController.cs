using API.helpers;
using API.requests;
using Microsoft.AspNetCore.Mvc;
using Services.products.interfaces;
using Shared.dtos.products;
using Shared.requests;
using System.ComponentModel.DataAnnotations;

namespace API.controllers.products
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _svc;

        public ProductsController(IProductsService svc)
        {
            _svc = svc;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] GetListRequest req)
            => await RequestExecutor.Execute(async () =>
            {
                var data = await _svc.GetProductsAsync(req);
                return ResponseBuilder.BuildOk(data);
            }, ModelState);

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById([FromRoute] ByIdRequest req)
            => await RequestExecutor.Execute(async () =>
            {
                var data = await _svc.GetProductByIdAsync(req.Id);
                return ResponseBuilder.BuildOk(data);
            }, ModelState);

        [HttpPost()]
        public async Task<IActionResult> PostProduct(
            [Required(ErrorMessage = "Информация о товаре обязательна")]
            [FromBody] ProductCreateDTO dto)
            => await RequestExecutor.Execute(async () =>
            {
                var id = await _svc.CreateProductAsync(dto);
                var data = await _svc.GetProductByIdAsync(id);
                return ResponseBuilder.BuildCreated(data);
            }, ModelState);

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(
            [FromRoute] ByIdRequest req,
            [Required(ErrorMessage = "Информация о товаре обязательна")]
            [FromBody] ProductUpdateDTO dto)
            => await RequestExecutor.Execute(async () =>
            {
                await _svc.UpdateProductByIdAsync(req.Id, dto);
                return ResponseBuilder.BuildOk<object?>(null);
            }, ModelState);

        [HttpPut("{id}/pricing")]
        public async Task<IActionResult> PutProductPricing(
            [FromRoute] ByIdRequest req,
            [Required(ErrorMessage = "Информация о стоимости товара обязательна")]
            [FromBody] ProductPricingUpdateDTO dto)
            => await RequestExecutor.Execute(async () =>
            {
                await _svc.UpdateProductPricingByIdAsync(req.Id, dto);
                return ResponseBuilder.BuildOk<object?>(null);
            }, ModelState);

        [HttpPut("{id}/relations")]
        public async Task<IActionResult> PutProductRelations(
            [FromRoute] ByIdRequest req,
            [Required(ErrorMessage = "Информация об отношениях товара обязательна")]
            [FromBody] ProductRelationsUpdateDTO dto)
            => await RequestExecutor.Execute(async () =>
            {
                await _svc.UpdateProductRelationsByIdAsync(req.Id, dto);
                return ResponseBuilder.BuildOk<object?>(null);
            }, ModelState);

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductById([FromRoute] ByIdRequest req)
            => await RequestExecutor.Execute(async () =>
            {
                await _svc.DeleteProductByIdAsync(req.Id);
                return ResponseBuilder.BuildOk<object?>(null);
            }, ModelState);

        [HttpGet("count")]
        public async Task<IActionResult> GetProductsCount()
            => await RequestExecutor.Execute(async () =>
            {
                var data = await _svc.GetProductsCountAsync();
                return ResponseBuilder.BuildOk(data);
            }, ModelState);
    }
}
