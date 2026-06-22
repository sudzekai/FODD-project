using Microsoft.AspNetCore.Mvc.RazorPages;
using Clients.webClients.tags.interfaces;
using Shared.dtos.tags;
using Shared.requests;

namespace WEB.Pages.Admin.Tags
{
    public class IndexModel : PageModel
    {
        private readonly ITagsWebClient _tagsClient;

        public IndexModel(ITagsWebClient tagsClient)
        {
            _tagsClient = tagsClient;
        }

        public List<TagDTO> Tags { get; set; } = new();

        public async Task OnGetAsync(CancellationToken ct = default)
        {
            try
            {
                var token = HttpContext.User.FindFirst("Token")?.Value;

                Tags = await _tagsClient.GetTagsAsync(
                    new GetListRequest(),
                    token,
                    ct
                );
            }
            catch (Exception ex)
            {
                Tags = new List<TagDTO>();
                TempData["Error"] = ex.Message;
            }
        }
    }
}