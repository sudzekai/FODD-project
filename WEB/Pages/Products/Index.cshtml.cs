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
        private string? Token => HttpContext.User.FindFirst("Token")?.Value;


        private readonly IProductsWebClient _products;
        private readonly ICategoriesWebClient _categories;
        private readonly ISuppliersWebClient _suppliers;
        private readonly IManufacturersWebClient _manufacturers;

        public IndexModel(
            IProductsWebClient products,
            ISuppliersWebClient suppliers,
            IManufacturersWebClient manufacturers,
            ICategoriesWebClient categories)
        {
            _products = products;
            _suppliers = suppliers;
            _manufacturers = manufacturers;
            _categories = categories;
        }

        public SelectList Suppliers { get; set; } = default!;
        public SelectList Manufacturers { get; set; } = default!;
        public SelectList Categories { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public bool IsOnlyDiscounted { get; set; }

        [BindProperty(SupportsGet = true)]
        public GetProductsListRequest GetRequest { get; set; } = new();

        public List<ProductSimpleDTO> Products { get; set; } = [];

        public async Task<IActionResult?> OnGet(bool? isReset)
        {
            if (isReset == true)
                return RedirectToPage();

            GetRequest.DiscountsOnly = IsOnlyDiscounted ? "true" : "false";

            try
            {
                var suppliers = await _suppliers.GetSuppliersAsync(new(), Token);
                var manufacturers = await _manufacturers.GetManufacturersAsync(new(), Token);
                var categories = await _categories.GetCategoriesAsync(new(), Token);
                var products = await _products.GetProductsAsync(GetRequest, Token);

                Suppliers = new SelectList(suppliers, "Id", "Name");
                Manufacturers = new SelectList(manufacturers, "Id", "Name");
                Categories = new SelectList(categories, "Id", "Name");

                Products = products;
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return Page();
        }
    }
}