using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Clients.webClients.tags.interfaces;
using Shared.dtos.tags;

namespace WEB.Pages.Admin.Tags
{
    public class CreateModel : PageModel
    {
        private readonly ITagsWebClient _tagsClient;

        public CreateModel(ITagsWebClient tagsClient)
        {
            _tagsClient = tagsClient;
        }

        [BindProperty]
        public TagWriteDTO Tag { get; set; } = new();

        public Task OnGetAsync() => Task.CompletedTask;

        public async Task<IActionResult> OnPostAsync(CancellationToken ct = default)
        {
            try
            {
                var token = HttpContext.User.FindFirst("Token")?.Value;

                var created = await _tagsClient.CreateTagAsync(
                    Tag,
                    token,
                    ct
                );

                return Redirect($"/admin/tags/{created.Id}");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return Page();
            }
        }
    }
}