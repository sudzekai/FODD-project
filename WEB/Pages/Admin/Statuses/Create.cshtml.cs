using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Clients.webClients.statuses.interfaces;
using Shared.dtos.statuses;

namespace WEB.Pages.Admin.Statuses
{
    public class CreateModel : PageModel
    {
        private readonly IStatusesWebClient _statusesClient;

        public CreateModel(IStatusesWebClient statusesClient)
        {
            _statusesClient = statusesClient;
        }

        [BindProperty]
        public StatusWriteDTO Status { get; set; } = new();

        public Task OnGetAsync() => Task.CompletedTask;

        public async Task<IActionResult> OnPostAsync(CancellationToken ct = default)
        {
            try
            {
                var token = HttpContext.User.FindFirst("Token")?.Value;

                var created = await _statusesClient.CreateStatusAsync(
                    Status,
                    token,
                    ct
                );

                return Redirect($"/admin/statuses/{created.Id}");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return Page();
            }
        }
    }
}