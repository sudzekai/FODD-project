using Clients.webClients.users.interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shared.dtos.users;
using System.Security.Claims;

namespace WEB.Pages
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        private readonly IUsersWebClient _userWebClient;

        public ProfileModel(IUsersWebClient userWebClient)
        {
            _userWebClient = userWebClient;
        }

        public UserDTO? UserData { get; private set; }

        private string? Token =>
            HttpContext.User.FindFirst("Token")?.Value;

        public async Task OnGetAsync()
        {
            var idClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(idClaim, out var userId))
                return;
            
            try
            {
                UserData = await _userWebClient.GetUserByIdAsync(userId, Token);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
        }

        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await HttpContext.SignOutAsync("Cookies");
            return RedirectToPage("/Login");
        }
    }
}