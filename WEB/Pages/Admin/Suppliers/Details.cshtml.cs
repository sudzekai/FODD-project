using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Clients.webClients.suppliers.interfaces;
using Shared.dtos.suppliers;

namespace WEB.Pages.Admin.Suppliers
{
    public class DetailsModel : PageModel
    {
        private readonly ISuppliersWebClient _suppliersClient;

        public DetailsModel(ISuppliersWebClient suppliersClient)
        {
            _suppliersClient = suppliersClient;
        }

        [BindProperty]
        public SupplierDTO Supplier { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id, CancellationToken ct = default)
        {
            try
            {
                var token = HttpContext.User.FindFirst("Token")?.Value;

                Supplier = await _suppliersClient.GetSupplierByIdAsync(id, token, ct);

                return Page();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToPage("/Admin/Suppliers/Index");
            }
        }

        public async Task<IActionResult> OnPostAsync(bool? isDeleting = false, CancellationToken ct = default)
        {
            var token = HttpContext.User.FindFirst("Token")?.Value;


            if (isDeleting == true)
                try
                {
                    await _suppliersClient.DeleteSupplierAsync(Supplier.Id, token);
                    return RedirectToPage("/Admin/Suppliers/Index");
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                    return Page();
                }

            try
            {

                var dto = new SupplierWriteDTO
                {
                    Name = Supplier.Name
                };

                await _suppliersClient.UpdateSupplierAsync(
                    Supplier.Id,
                    dto,
                    token,
                    ct
                );

                return RedirectToPage("/Admin/Suppliers/Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return Page();
            }
        }
    }
}