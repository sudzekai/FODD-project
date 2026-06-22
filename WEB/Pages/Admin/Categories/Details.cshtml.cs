using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Clients.webClients.categories.interfaces;
using Shared.dtos.categories;

namespace WEB.Pages.Admin.Categories
{
    public class DetailsModel : PageModel
    {
        private readonly ICategoriesWebClient _categoriesClient;

        public DetailsModel(ICategoriesWebClient categoriesClient)
        {
            _categoriesClient = categoriesClient;
        }

        [BindProperty]
        public CategoryDTO Category { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id, CancellationToken ct = default)
        {
            try
            {
                var token = HttpContext.User.FindFirst("Token")?.Value;

                Category = await _categoriesClient.GetCategoryByIdAsync(id, token, ct);

                return Page();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToPage("/Admin/Categories/Index");
            }
        }

        public async Task<IActionResult> OnPostAsync(bool? isDeleting = false, CancellationToken ct = default)
        {
            var token = HttpContext.User.FindFirst("Token")?.Value;

            if (isDeleting == true)
                try
                {
                    await _categoriesClient.DeleteCategoryAsync(Category.Id, token);
                    return RedirectToPage("/Admin/Categories/Index");
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                    return Page();
                }

            try
            {

                var dto = new CategoryWriteDTO
                {
                    Name = Category.Name
                };

                await _categoriesClient.UpdateCategoryAsync(Category.Id, dto, token, ct);

                return RedirectToPage("/Admin/Categories/Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return Page();
            }
        }
    }
}