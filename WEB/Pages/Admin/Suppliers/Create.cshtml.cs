using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Clients.webClients.suppliers.interfaces;
using Shared.dtos.suppliers;

namespace WEB.Pages.Admin.Suppliers
{
    public class CreateModel : PageModel
    {
        private readonly ISuppliersWebClient _suppliersClient;

        public CreateModel(ISuppliersWebClient suppliersClient)
        {
            _suppliersClient = suppliersClient;
        }

        [BindProperty]
        public SupplierWriteDTO Supplier { get; set; } = new();

        public Task OnGetAsync() => Task.CompletedTask;

        public async Task<IActionResult> OnPostAsync(CancellationToken ct = default)
        {
            try
            {
                var token = HttpContext.User.FindFirst("Token")?.Value;

                var created = await _suppliersClient.CreateSupplierAsync(
                    Supplier,
                    token,
                    ct
                );

                return Redirect($"/admin/suppliers/{created.Id}");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return Page();
            }
        }
    }
}