using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Clients.webClients.manufacturers.interfaces;
using Shared.dtos.manufacturers;

namespace WEB.Pages.Admin.Manufacturers
{
    public class DetailsModel : PageModel
    {
        private readonly IManufacturersWebClient _manufacturersClient;

        public DetailsModel(IManufacturersWebClient manufacturersClient)
        {
            _manufacturersClient = manufacturersClient;
        }

        [BindProperty]
        public ManufacturerDTO Manufacturer { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id, CancellationToken ct = default)
        {
            try
            {
                var token = HttpContext.User.FindFirst("Token")?.Value;

                Manufacturer = await _manufacturersClient.GetManufacturerByIdAsync(id, token, ct);

                return Page();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToPage("/Admin/Manufacturers/Index");
            }
        }

        public async Task<IActionResult> OnPostAsync(bool? isDeleting = false, CancellationToken ct = default)
        {
            var token = HttpContext.User.FindFirst("Token")?.Value;

            if (isDeleting == true)
                try
                {
                    await _manufacturersClient.DeleteManufacturerAsync(Manufacturer.Id, token);
                    return RedirectToPage("/Admin/Manufacturers/Index");
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                    return Page();
                }

            try
            {
                var dto = new ManufacturerWriteDTO
                {
                    Name = Manufacturer.Name
                };

                await _manufacturersClient.UpdateManufacturerAsync(
                    Manufacturer.Id,
                    dto,
                    token,
                    ct
                );

                return RedirectToPage("/Admin/Manufacturers/Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return Page();
            }
        }
    }
}