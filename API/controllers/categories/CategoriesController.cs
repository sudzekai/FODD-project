using API.helpers;
using API.requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.categories.interfaces;
using Shared.dtos.categories;
using Shared.requests;
using System.ComponentModel.DataAnnotations;

namespace API.controllers.Categories
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService _svc;

        public CategoriesController(ICategoriesService svc)
        {
            _svc = svc;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories([FromQuery] GetListRequest req)
            => await RequestExecutor.Execute(async () =>
            {
                var data = await _svc.GetCategoriesAsync(req);
                return ResponseBuilder.BuildOk(data);
            }, ModelState);

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] ByIdRequest req)
            => await RequestExecutor.Execute(async () =>
            {
                var data = await _svc.GetCategoryByIdAsync(req.Id);
                return ResponseBuilder.BuildOk(data);
            }, ModelState);

        [HttpPost()]
        [Authorize(Roles = "Менеджер,Администратор")]
        public async Task<IActionResult> PostCategory(
            [Required(ErrorMessage = "Информация о категории обязательна")]
            [FromBody] CategoryWriteDTO dto)
            => await RequestExecutor.Execute(async () =>
            {
                var id = await _svc.CreateCategoryAsync(dto);
                var data = await _svc.GetCategoryByIdAsync(id);
                return ResponseBuilder.BuildCreated(data);
            }, ModelState);

        [HttpPut("{id}")]
        [Authorize(Roles = "Менеджер,Администратор")]
        public async Task<IActionResult> PutCategory(
            [FromRoute] ByIdRequest req,
            [Required(ErrorMessage = "Информация о категории обязательна")]
            [FromBody] CategoryWriteDTO dto)
            => await RequestExecutor.Execute(async () =>
            {
                await _svc.UpdateCategoryByIdAsync(req.Id, dto);
                return ResponseBuilder.BuildOk<object?>(null);
            }, ModelState);

        [HttpDelete("{id}")]
        [Authorize(Roles = "Администратор")]
        public async Task<IActionResult> DeleteCategoryById([FromRoute] ByIdRequest req)
            => await RequestExecutor.Execute(async () =>
            {
                await _svc.DeleteCategoryByIdAsync(req.Id);
                return ResponseBuilder.BuildOk<object?>(null);
            }, ModelState);

        [HttpGet("count")]
        public async Task<IActionResult> GetCategoriesCount()
            => await RequestExecutor.Execute(async () =>
            {
                var data = await _svc.GetCategoriesCountAsync();
                return ResponseBuilder.BuildOk(data);
            }, ModelState);
    }
}
