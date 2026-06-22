using Clients.webClients.categories.interfaces;
using Clients.webClients.manufacturers.interfaces;
using Clients.webClients.products.interfaces;
using Clients.webClients.suppliers.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shared.dtos.products;

namespace WEB.Pages.Products
{
    public class DetailsModel : PageModel
    {
        private string? Token => HttpContext.User.FindFirst("Token")?.Value;

        private readonly IProductsWebClient _products;
        private readonly ICategoriesWebClient _categories;
        private readonly IManufacturersWebClient _manufacturers;
        private readonly ISuppliersWebClient _suppliers;

        public DetailsModel(
            IProductsWebClient products,
            ICategoriesWebClient categories,
            IManufacturersWebClient manufacturers,
            ISuppliersWebClient suppliers)
        {
            _products = products;
            _categories = categories;
            _manufacturers = manufacturers;
            _suppliers = suppliers;
        }

        public ProductDTO? Product { get; private set; }

        public string? SupplierName { get; private set; }
        public string? ManufacturerName { get; private set; }
        public string? CategoryName { get; private set; }
        public decimal FinalPrice { get; private set; }

        public async Task<IActionResult> OnGet(int id)
        {
            try
            {
                var token = Token;

                Product = await _products.GetProductByIdAsync(id, token);

                if (Product == null)
                    return NotFound();

                if (Product.CategoryId is int categoryId)
                {
                    var category = await _categories.GetCategoryByIdAsync(categoryId, token);
                    CategoryName = category?.Name;
                }

                if (Product.ManufacturerId is int manufacturerId)
                {
                    var manufacturer = await _manufacturers.GetManufacturerByIdAsync(manufacturerId, token);
                    ManufacturerName = manufacturer?.Name;
                }

                if (Product.SupplierId is int supplierId)
                {
                    var supplier = await _suppliers.GetSupplierByIdAsync(supplierId, token);
                    SupplierName = supplier?.Name;
                }

                FinalPrice = Product.Price * (1 - Product.Discount / 100m);

                ViewData["Title"] = Product.Name;

                return Page();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return Redirect("/index");
            }
        }
    }
}