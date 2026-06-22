using Microsoft.AspNetCore.Mvc.RazorPages;
using Clients.webClients.manufacturers.interfaces;
using Shared.dtos.manufacturers;
using Shared.requests;

namespace WEB.Pages.Admin.Manufacturers
{
    public class IndexModel : PageModel
    {
        private readonly IManufacturersWebClient _manufacturersClient;

        public IndexModel(IManufacturersWebClient manufacturersClient)
        {
            _manufacturersClient = manufacturersClient;
        }

        public List<ManufacturerDTO> Manufacturers { get; set; } = new();

        public async Task OnGetAsync(CancellationToken ct = default)
        {
            try
            {
                var token = HttpContext.User.FindFirst("Token")?.Value;

                Manufacturers = await _manufacturersClient.GetManufacturersAsync(
                    new GetListRequest(),
                    token,
                    ct
                );
            }
            catch (Exception ex)
            {
                Manufacturers = new List<ManufacturerDTO>();
                TempData["Error"] = ex.Message;
            }
        }
    }
}