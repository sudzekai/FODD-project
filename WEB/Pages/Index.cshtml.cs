using Clients.webClients.users.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shared.dtos.users;
using Shared.requests;

namespace WEB.Pages
{
    public class IndexModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync(string? search)
        {
            return Redirect("/products");
        }
    }
}