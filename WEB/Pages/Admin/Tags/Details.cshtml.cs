using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Clients.webClients.tags.interfaces;
using Shared.dtos.tags;

namespace WEB.Pages.Admin.Tags
{
    public class DetailsModel : PageModel
    {
        private readonly ITagsWebClient _tagsClient;

        public DetailsModel(ITagsWebClient tagsClient)
        {
            _tagsClient = tagsClient;
        }

        [BindProperty]
        public TagDTO Tag { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id, CancellationToken ct = default)
        {
            try
            {
                var token = HttpContext.User.FindFirst("Token")?.Value;

                Tag = await _tagsClient.GetTagByIdAsync(id, token, ct);

                return Page();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToPage("/Admin/Tags/Index");
            }
        }

        public async Task<IActionResult> OnPostAsync(bool? isDeleting = false, CancellationToken ct = default)
        {
            var token = HttpContext.User.FindFirst("Token")?.Value;

            if (isDeleting == true)
                try
                {
                    await _tagsClient.DeleteTagAsync(Tag.Id, token);
                    return RedirectToPage("/Admin/Tags/Index");
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                    return Page();
                }

            try
            {

                var dto = new TagWriteDTO
                {
                    Name = Tag.Name
                };

                await _tagsClient.UpdateTagAsync(
                    Tag.Id,
                    dto,
                    token,
                    ct
                );

                return RedirectToPage("/Admin/Tags/Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return Page();
            }
        }
    }
}