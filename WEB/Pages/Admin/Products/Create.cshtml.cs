using Clients.webClients.categories.interfaces;
using Clients.webClients.manufacturers.interfaces;
using Clients.webClients.products.interfaces;
using Clients.webClients.suppliers.interfaces;
using Clients.webClients.tags.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shared.dtos.products;
using Shared.dtos.tags;
using Shared.requests;

namespace WEB.Pages.Admin.Products
{
    public class CreateModel : PageModel
    {
        private readonly IProductsWebClient _products;
        private readonly ICategoriesWebClient _categories;
        private readonly ISuppliersWebClient _suppliers;
        private readonly IManufacturersWebClient _manufacturers;
        private readonly ITagsWebClient _tags;
        private readonly IProductTagsWebClient _productTags;

        public CreateModel(
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
        public ProductCreateDTO Product { get; set; } = new();

        [BindProperty]
        public List<int> SelectedTags { get; set; } = new();

        public SelectList Categories { get; set; } = default!;
        public SelectList Suppliers { get; set; } = default!;
        public SelectList Manufacturers { get; set; } = default!;
        public SelectList TagsSelectList { get; set; } = default!;

        public async Task OnGetAsync(CancellationToken ct = default)
        {
            var token = HttpContext.User.FindFirst("Token")?.Value;

            var categories = await _categories.GetCategoriesAsync(new(), token, ct);
            var suppliers = await _suppliers.GetSuppliersAsync(new(), token, ct);
            var manufacturers = await _manufacturers.GetManufacturersAsync(new(), token, ct);
            var tags = await _tags.GetTagsAsync(new GetListRequest(), token, ct);

            Categories = new SelectList(categories, "Id", "Name");
            Suppliers = new SelectList(suppliers, "Id", "Name");
            Manufacturers = new SelectList(manufacturers, "Id", "Name");
            TagsSelectList = new SelectList(tags, "Id", "Name");
        }

        public async Task<IActionResult> OnPostAsync(CancellationToken ct = default)
        {
            try
            {
                var token = HttpContext.User.FindFirst("Token")?.Value;

                var created = await _products.CreateProductAsync(Product, token, ct);

                if (created == null)
                {
                    TempData["Error"] = "Не удалось создать товар";
                    return Page();
                }

                if (SelectedTags.Any())
                {
                    var dto = new ProductTagsUpdateDTO(SelectedTags);

                    await _productTags.AddProductTagsAsync(created.Id, dto, token, ct);
                }

                return RedirectToPage("/Admin/Products/Details", new { id = created.Id });
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return Page();
            }
        }
    }
}