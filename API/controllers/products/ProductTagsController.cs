using API.helpers;
using API.requests;
using Microsoft.AspNetCore.Mvc;
using Services.products.interfaces;
using Shared.dtos.products;
using System.ComponentModel.DataAnnotations;

namespace API.controllers.products
{
    [ApiController]
    [Route("products")]
    public class ProductTagsController : ControllerBase
    {
        private readonly IProductTagsService _productTagsSvc;

        public ProductTagsController(IProductTagsService productTagsSvc)
        {
            _productTagsSvc = productTagsSvc;
        }

        [HttpGet("{id}/tags")]
        public async Task<IActionResult> GetProductTagsById([FromRoute] ByIdRequest req)
            => await RequestExecutor.Execute(async () =>
            {
                var data = await _productTagsSvc.GetProductTagsByProductIdAsync(req.Id);
                return ResponseBuilder.BuildOk(data);
            }, ModelState);

        [HttpPost("{id}/tags/add")]
        public async Task<IActionResult> AddProductTagById(
            [FromRoute] ByIdRequest req,
            [FromBody]
            [Required(ErrorMessage = "Тэги обязательны")]
            ProductTagsUpdateDTO dto)
            => await RequestExecutor.Execute(async () =>
            {
                var data = _productTagsSvc.AddBatchProductTagsById(req.Id, dto);
                return ResponseBuilder.BuildOk(data);
            }, ModelState);

        [HttpPost("{id}/tags/remove")]
        public async Task<IActionResult> RemoveProductTagById(
            [FromRoute] ByIdRequest req,
            [FromBody]
            [Required(ErrorMessage = "Тэги обязательны")]
            ProductTagsUpdateDTO dto)
            => await RequestExecutor.Execute(async () =>
            {
                var data = _productTagsSvc.DeleteBatchProductTagsById(req.Id, dto);
                return ResponseBuilder.BuildOk(data);
            }, ModelState);

        [HttpGet("{id}/tags/count")]
        public async Task<IActionResult> GetProductTagsCountById([FromRoute] ByIdRequest req)
            => await RequestExecutor.Execute(async () =>
            {
                var data = await _productTagsSvc.GetProductTagsCountByProductIdAsync(req.Id);
                return ResponseBuilder.BuildOk(data);

            }, ModelState);
    }
}
