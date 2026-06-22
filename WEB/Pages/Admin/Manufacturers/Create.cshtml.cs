using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Clients.webClients.manufacturers.interfaces;
using Shared.dtos.manufacturers;

namespace WEB.Pages.Admin.Manufacturers
{
    public class CreateModel : PageModel
    {
        private readonly IManufacturersWebClient _manufacturersClient;

        public CreateModel(IManufacturersWebClient manufacturersClient)
        {
            _manufacturersClient = manufacturersClient;
        }

        [BindProperty]
        public ManufacturerWriteDTO Manufacturer { get; set; } = new();

        public Task OnGetAsync() => Task.CompletedTask;

        public async Task<IActionResult> OnPostAsync(CancellationToken ct = default)
        {
            try
            {
                var token = HttpContext.User.FindFirst("Token")?.Value;

                var created = await _manufacturersClient.CreateManufacturerAsync(
                    Manufacturer,
                    token,
                    ct
                );

                return Redirect($"/admin/manufacturers/{created.Id}");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return Page();
            }
        }
    }
}