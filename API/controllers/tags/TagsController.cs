using API.helpers;
using API.requests;
using Microsoft.AspNetCore.Mvc;
using Services.tags.interfaces;
using Shared.dtos.tags;
using Shared.requests;
using System.ComponentModel.DataAnnotations;

namespace API.controllers.tags
{
    [ApiController]
    [Route("[controller]")]
    public class TagsController : ControllerBase
    {
        private readonly ITagsService _svc;

        public TagsController(ITagsService svc)
        {
            _svc = svc;
        }

        [HttpGet]
        public async Task<IActionResult> GetTags([FromRoute] GetListRequest req)
            => await RequestExecutor.Execute(async () =>
            {
                var data = await _svc.GetTagsAsync(req);
                return ResponseBuilder.BuildOk(data);
            }, ModelState);

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTagById([FromRoute] ByIdRequest req)
            => await RequestExecutor.Execute(async () =>
            {
                var data = await _svc.GetTagByIdAsync(req.Id);
                return ResponseBuilder.BuildOk(data);
            }, ModelState);

        [HttpPost()]
        public async Task<IActionResult> PostTag(
            [Required(ErrorMessage = "Информация о тэге обязательна")]
            [FromBody] TagWriteDTO dto)
            => await RequestExecutor.Execute(async () =>
            {
                var id = await _svc.CreateTagAsync(dto);
                var data = await _svc.GetTagByIdAsync(id);
                return ResponseBuilder.BuildCreated(data);
            }, ModelState);

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTag(
            [FromRoute] ByIdRequest req,
            [Required(ErrorMessage = "Информация о тэге обязательна")]
            [FromBody] TagWriteDTO dto)
            => await RequestExecutor.Execute(async () =>
            {
                await _svc.UpdateTagByIdAsync(req.Id, dto);
                return ResponseBuilder.BuildOk<object?>(null);
            }, ModelState);

        [HttpDelete("{id}")]
        public async Task<IActionResult> PutTagRelations([FromRoute] ByIdRequest req)
            => await RequestExecutor.Execute(async () =>
            {
                await _svc.DeleteTagByIdAsync(req.Id);
                return ResponseBuilder.BuildOk<object?>(null);
            }, ModelState);

        [HttpGet("count")]
        public async Task<IActionResult> GetTagsCount()
            => await RequestExecutor.Execute(async () =>
            {
                var data = await _svc.GetTagsCountAsync();
                return ResponseBuilder.BuildOk(data);
            }, ModelState);
    }
}
