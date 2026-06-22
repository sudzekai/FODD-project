using Microsoft.AspNetCore.Mvc.RazorPages;
using Clients.webClients.categories.interfaces;
using Shared.dtos.categories;
using System.Security.Claims;
using Shared.requests;

namespace WEB.Pages.Admin.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ICategoriesWebClient _categoriesClient;

        public IndexModel(ICategoriesWebClient categoriesClient)
        {
            _categoriesClient = categoriesClient;
        }

        public List<CategoryDTO> Categories { get; set; } = new();

        public async Task OnGetAsync(CancellationToken ct = default)
        {
            try
            {
                var token = HttpContext.User.FindFirst("Token")?.Value;

                Categories = await _categoriesClient.GetCategoriesAsync(
                    new GetListRequest(),
                    token,
                    ct
                );
            }
            catch (Exception ex)
            {
                Categories = new List<CategoryDTO>();
                TempData["Error"] = ex.Message;
            }
        }
    }
}