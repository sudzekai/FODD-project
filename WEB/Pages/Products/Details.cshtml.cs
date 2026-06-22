using Clients.webClients.categories.interfaces;
using Clients.webClients.manufacturers.interfaces;
using Clients.webClients.products.interfaces;
using Clients.webClients.suppliers.interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shared.dtos.products;

namespace WEB.Pages.Products
{
    public class DetailsModel : PageModel
    {
        private readonly IProductsWebClient _webClient;
        private readonly ICategoriesWebClient _categoriesWebClient;
        private readonly IManufacturersWebClient _manufacturersWebClient;
        private readonly ISuppliersWebClient _suppliersWebClient;
        public DetailsModel(IProductsWebClient webClient, ICategoriesWebClient categoriesWebClient, IManufacturersWebClient manufacturersWebClient, ISuppliersWebClient suppliersWebClient)
        {
            _webClient = webClient;
            _categoriesWebClient = categoriesWebClient;
            _manufacturersWebClient = manufacturersWebClient;
            _suppliersWebClient = suppliersWebClient;
        }

        public int Id { get; private set; }

        public string SupplierName { get; private set; }
        public string ManufacturerName { get; private set; }
        public string CategoryName { get; private set; }

        public ProductDTO Product { get; private set; } = new();

        public async Task OnGet(int id)
        {
            Id = id;
            Product = await _webClient.GetProductByIdAsync(Id);
            ViewData["Title"] = Product.Name;

            CategoryName = (await _categoriesWebClient.GetCategoryByIdAsync(Product.CategoryId.Value)).Name;
            ManufacturerName = (await _manufacturersWebClient.GetManufacturerByIdAsync(Product.ManufacturerId.Value)).Name;
            SupplierName = (await _suppliersWebClient.GetSupplierByIdAsync(Product.SupplierId.Value)).Name;
        }
    }
}
