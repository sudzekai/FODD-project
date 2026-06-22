using Clients.webClients.categories.interfaces;
using Clients.webClients.manufacturers.interfaces;
using Clients.webClients.products.interfaces;
using Clients.webClients.suppliers.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shared.dtos.categories;
using Shared.dtos.manufacturers;
using Shared.dtos.products;
using Shared.dtos.suppliers;

namespace WEB.Pages.Products
{
    [Authorize(Roles = "Менеджер,Администратор")]
    public class CreateModel : PageModel
    {
        private readonly IProductsWebClient _webClient;
        private readonly ICategoriesWebClient _categoriesWebClient;
        private readonly IManufacturersWebClient _manufacturersWebClient;
        private readonly ISuppliersWebClient _suppliersWebClient;

        public CreateModel(IProductsWebClient webClient, ICategoriesWebClient categoriesWebClient, IManufacturersWebClient manufacturersWebClient, ISuppliersWebClient suppliersWebClient)
        {
            _webClient = webClient;
            _categoriesWebClient = categoriesWebClient;
            _manufacturersWebClient = manufacturersWebClient;
            _suppliersWebClient = suppliersWebClient;
        }

        [BindProperty]
        public ProductCreateDTO Product { get; set; } = new("", 1m, 0, "шт.", 0, 40, "", "", 0, 0, 0);

        public SelectList Suppliers { get; set; }
        public SelectList Categories { get; set; }
        public SelectList Manufacturers { get; set; }

        public async Task OnGet()
        {
            var suppliers = await _suppliersWebClient.GetSuppliersAsync(new());
            var manufacturers = await _manufacturersWebClient.GetManufacturersAsync(new());
            var categories = await _categoriesWebClient.GetCategoriesAsync(new());

            Suppliers = new SelectList(suppliers, "Id", "Name");
            Manufacturers = new SelectList(manufacturers, "Id", "Name");
            Categories = new SelectList(categories, "Id", "Name");
        }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            await _webClient.CreateProductAsync(Product);

            return RedirectToPage("/Index");
        }
    }
}
