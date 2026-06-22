using Clients.webClients.categories.interfaces;
using Clients.webClients.manufacturers.interfaces;
using Clients.webClients.products.interfaces;
using Clients.webClients.suppliers.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shared.dtos.products;
using Shared.requests;

namespace WEB.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly IProductsWebClient _webClient;

        private readonly ICategoriesWebClient _categoriesWebClient;
        private readonly ISuppliersWebClient _suppliersWebClient;
        private readonly IManufacturersWebClient _manufacturersWebClient;


        public IndexModel(IProductsWebClient webClient, ISuppliersWebClient suppliersWebClient, IManufacturersWebClient manufacturersWebClient, ICategoriesWebClient categoriesWebClient)
        {
            _webClient = webClient;
            _suppliersWebClient = suppliersWebClient;
            _manufacturersWebClient = manufacturersWebClient;
            _categoriesWebClient = categoriesWebClient;
        }

        public SelectList Suppliers { get; set; }

        public SelectList Manufacturers { get; set; }

        public SelectList Categories { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool IsOnlyDiscounted { get; set; } = false;

        [BindProperty(SupportsGet = true)]
        public GetProductsListRequest GetRequest { get; set; } = new();

        public List<ProductSimpleDTO> Products { get; set; } = new();


        public async Task<IActionResult?> OnGet(bool? isReset)
        {
            if (isReset == true)
                return RedirectToPage("/Products/Index");

            var suppliers = await _suppliersWebClient.GetSuppliersAsync(new());
            Suppliers = new(suppliers, "Id", "Name");

            var manufacturers = await _manufacturersWebClient.GetManufacturersAsync(new());
            Manufacturers = new(manufacturers, "Id", "Name");

            var categories = await _categoriesWebClient.GetCategoriesAsync(new());
            Categories = new(categories, "Id", "Name");

            GetRequest.DiscountsOnly = IsOnlyDiscounted ? "true" : "false";

            Products = await _webClient.GetProductsAsync(GetRequest);

            return Page();
        }
    }
}
