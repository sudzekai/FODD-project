using Microsoft.AspNetCore.Mvc.RazorPages;
using Clients.webClients.suppliers.interfaces;
using Shared.dtos.suppliers;
using Shared.requests;

namespace WEB.Pages.Admin.Suppliers
{
    public class IndexModel : PageModel
    {
        private readonly ISuppliersWebClient _suppliersClient;

        public IndexModel(ISuppliersWebClient suppliersClient)
        {
            _suppliersClient = suppliersClient;
        }

        public List<SupplierDTO> Suppliers { get; set; } = new();

        public async Task OnGetAsync(CancellationToken ct = default)
        {
            try
            {
                var token = HttpContext.User.FindFirst("Token")?.Value;

                Suppliers = await _suppliersClient.GetSuppliersAsync(
                    new GetListRequest(),
                    token,
                    ct
                );
            }
            catch (Exception ex)
            {
                Suppliers = new List<SupplierDTO>();
                TempData["Error"] = ex.Message;
            }
        }
    }
}