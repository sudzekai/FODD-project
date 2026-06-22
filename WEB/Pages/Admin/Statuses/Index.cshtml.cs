using Microsoft.AspNetCore.Mvc.RazorPages;
using Clients.webClients.statuses.interfaces;
using Shared.dtos.statuses;

namespace WEB.Pages.Admin.Statuses
{
    public class IndexModel : PageModel
    {
        private readonly IStatusesWebClient _statusesClient;

        public IndexModel(IStatusesWebClient statusesClient)
        {
            _statusesClient = statusesClient;
        }

        public List<StatusDTO> Statuses { get; set; } = new();

        public async Task OnGetAsync(CancellationToken ct = default)
        {
            try
            {
                var token = HttpContext.User.FindFirst("Token")?.Value;

                Statuses = await _statusesClient.GetStatusesAsync(token, ct);
            }
            catch (Exception ex)
            {
                Statuses = new List<StatusDTO>();
                TempData["Error"] = ex.Message;
            }
        }
    }
}