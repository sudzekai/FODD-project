using Clients.webClients.categories.interfaces;
using Clients.webClients.manufacturers.interfaces;
using Clients.webClients.products.interfaces;
using Clients.webClients.suppliers.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shared.dtos.products;
using Shared.requests;

namespace WEB.Pages.Admin.Products
{
    public class IndexModel : PageModel
    {
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

        public List<ProductSimpleDTO> Products { get; set; } = new();

        public SelectList Suppliers { get; set; } = default!;
        public SelectList Manufacturers { get; set; } = default!;
        public SelectList Categories { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public GetProductsListRequest GetRequest { get; set; } = new();

        public async Task OnGetAsync()
        {
            var token = HttpContext.User.FindFirst("Token")?.Value;

            try
            {
                var suppliersTask = _suppliers.GetSuppliersAsync(new(), token);
                var manufacturersTask = _manufacturers.GetManufacturersAsync(new(), token);
                var categoriesTask = _categories.GetCategoriesAsync(new(), token);
                var productsTask = _products.GetProductsAsync(GetRequest, token);

                await Task.WhenAll(suppliersTask, manufacturersTask, categoriesTask, productsTask);

                Suppliers = new SelectList(suppliersTask.Result, "Id", "Name");
                Manufacturers = new SelectList(manufacturersTask.Result, "Id", "Name");
                Categories = new SelectList(categoriesTask.Result, "Id", "Name");
                Products = productsTask.Result;
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;

                Suppliers = new SelectList(new List<object>(), "Id", "Name");
                Manufacturers = new SelectList(new List<object>(), "Id", "Name");
                Categories = new SelectList(new List<object>(), "Id", "Name");
                Products = new List<ProductSimpleDTO>();
            }
        }
    }
}