using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Clients.webClients.categories.interfaces;
using Shared.dtos.categories;

namespace WEB.Pages.Admin.Categories
{
    public class CreateModel : PageModel
    {
        private readonly ICategoriesWebClient _categoriesClient;

        public CreateModel(ICategoriesWebClient categoriesClient)
        {
            _categoriesClient = categoriesClient;
        }

        [BindProperty]
        public CategoryWriteDTO Category { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(CancellationToken ct = default)
        {
            try
            {
                var token = HttpContext.User.FindFirst("Token")?.Value;

                var created = await _categoriesClient.CreateCategoryAsync(
                    Category,
                    token,
                    ct
                );

                return Redirect($"/admin/categories/{created.Id}");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return Page();
            }
        }
    }
}