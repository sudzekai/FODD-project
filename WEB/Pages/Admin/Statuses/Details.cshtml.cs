using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Clients.webClients.statuses.interfaces;
using Shared.dtos.statuses;

namespace WEB.Pages.Admin.Statuses
{
    public class DetailsModel : PageModel
    {
        private readonly IStatusesWebClient _statusesClient;

        public DetailsModel(IStatusesWebClient statusesClient)
        {
            _statusesClient = statusesClient;
        }

        [BindProperty]
        public StatusDTO Status { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id, CancellationToken ct = default)
        {
            try
            {
                var token = HttpContext.User.FindFirst("Token")?.Value;

                Status = await _statusesClient.GetStatusByIdAsync(id, token, ct);

                return Page();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToPage("/Admin/Statuses/Index");
            }
        }

        public async Task<IActionResult> OnPostAsync(bool? isDeleting = false, CancellationToken ct = default)
        {
            var token = HttpContext.User.FindFirst("Token")?.Value;

            if (isDeleting == true)
                try
                {
                    await _statusesClient.DeleteStatusAsync(Status.Id, token);
                    return RedirectToPage("/Admin/Statuses/Index");
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                    return Page();
                }

            try
            {

                var dto = new StatusWriteDTO
                {
                    Name = Status.Name
                };

                await _statusesClient.UpdateStatusAsync(
                    Status.Id,
                    dto,
                    token,
                    ct
                );

                return RedirectToPage("/Admin/Statuses/Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return Page();
            }
        }
    }
}