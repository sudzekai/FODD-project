using Clients.webClients.categories.interfaces;
using Clients.webClients.manufacturers.interfaces;
using Clients.webClients.products.interfaces;
using Clients.webClients.suppliers.interfaces;
using Clients.webClients.tags.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shared.dtos.products;
using Shared.requests;

namespace WEB.Pages.Admin.Products
{
    public class DetailsModel : PageModel
    {
        private readonly IProductsWebClient _products;
        private readonly ICategoriesWebClient _categories;
        private readonly ISuppliersWebClient _suppliers;
        private readonly IManufacturersWebClient _manufacturers;
        private readonly ITagsWebClient _tags;
        private readonly IProductTagsWebClient _productTags;

        public DetailsModel(
            IProductsWebClient products,
            ICategoriesWebClient categories,
            ISuppliersWebClient suppliers,
            IManufacturersWebClient manufacturers,
            ITagsWebClient tags,
            IProductTagsWebClient productTags)
        {
            _products = products;
            _categories = categories;
            _suppliers = suppliers;
            _manufacturers = manufacturers;
            _tags = tags;
            _productTags = productTags;
        }

        [BindProperty]
        public ProductDTO Product { get; set; } = new();

        public List<string> ProductTags { get; set; } = new();

        public SelectList Categories { get; set; } = default!;
        public SelectList Suppliers { get; set; } = default!;
        public SelectList Manufacturers { get; set; } = default!;
        public SelectList TagsSelectList { get; set; } = default!;

        private string? Token => HttpContext.User.FindFirst("Token")?.Value;

        public async Task<IActionResult> OnGetAsync(int id, CancellationToken ct = default)
        {
            try
            {
                if (string.IsNullOrEmpty(Token))
                    throw new Exception("Token отсутствует");

                Product = await _products.GetProductByIdAsync(id, Token, ct);

                ProductTags = (await _productTags.GetProductTagsAsync(id, Token, ct))
                    .Select(x => x.Name)
                    .ToList();

                var categories = await _categories.GetCategoriesAsync(new(), Token, ct);
                var suppliers = await _suppliers.GetSuppliersAsync(new(), Token, ct);
                var manufacturers = await _manufacturers.GetManufacturersAsync(new(), Token, ct);
                var tags = await _tags.GetTagsAsync(new GetListRequest(), Token, ct);

                Categories = new SelectList(categories, "Id", "Name");
                Suppliers = new SelectList(suppliers, "Id", "Name");
                Manufacturers = new SelectList(manufacturers, "Id", "Name");
                TagsSelectList = new SelectList(tags, "Id", "Name");

                return Page();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToPage("/Admin/Products/Index");
            }
        }

        public async Task<IActionResult> OnPostUpdateAsync(CancellationToken ct)
        {
            try
            {
                await _products.UpdateProductAsync(Product.Id,
                    new ProductUpdateDTO(
                        Product.Name,
                        Product.Unit,
                        Product.Quantity,
                        Product.Size,
                        Product.Color,
                        Product.Description,
                        Product.CategoryId.Value),
                    Token!, ct);

                return RedirectToPage(new { id = Product.Id });
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToPage(new { id = Product.Id });
            }
        }

        public async Task<IActionResult> OnPostUpdatePricingAsync(CancellationToken ct)
        {
            try
            {
                await _products.UpdateProductPricingAsync(Product.Id,
                    new ProductPricingUpdateDTO(Product.Price, Product.Discount),
                    Token!, ct);

                return RedirectToPage(new { id = Product.Id });
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToPage(new { id = Product.Id });
            }
        }

        public async Task<IActionResult> OnPostUpdateRelationsAsync(CancellationToken ct)
        {
            try
            {
                await _products.UpdateProductRelationsAsync(Product.Id,
                    new ProductRelationsUpdateDTO(
                        Product.SupplierId ?? 0,
                        Product.ManufacturerId ?? 0),
                    Token!, ct);

                return RedirectToPage(new { id = Product.Id });
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToPage(new { id = Product.Id });
            }
        }

        public async Task<IActionResult> OnPostTagsAsync(string action, int tagId, CancellationToken ct)
        {
            try
            {
                var dto = new ProductTagsUpdateDTO(new List<int> { tagId });

                if (action == "add")
                    await _productTags.AddProductTagsAsync(Product.Id, dto, Token!, ct);

                if (action == "remove")
                    await _productTags.RemoveProductTagsAsync(Product.Id, dto, Token!, ct);

                return RedirectToPage(new { id = Product.Id });
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToPage(new { id = Product.Id });
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(CancellationToken ct)
        {
            try
            {
                await _products.DeleteProductAsync(Product.Id, Token!, ct);

                return RedirectToPage("/Admin/Products/Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToPage(new { id = Product.Id });
            }
        }
    }
}