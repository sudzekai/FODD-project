using Clients.webClients.auth.interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shared.requests;
using Shared.types.exceptions;
using System.Security.Claims;

namespace WEB.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IAuthWebClient _authClient;

        public LoginModel(IAuthWebClient authClient)
        {
            _authClient = authClient;
        }

        [BindProperty]
        public AuthRequest AuthRequest { get; set; } = new();

        public void OnGet() { }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            try
            {
                var response = await _authClient.LoginAsync(AuthRequest);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, response.UserId.ToString()),
                    new Claim(ClaimTypes.Name, response.FullName),
                    new Claim(ClaimTypes.Role, response.Role),
                    new Claim("Token", response.Token),
                };

                var identity = new ClaimsIdentity(claims, "Cookies");
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("Cookies", principal);

                return Redirect("/products");

            }
            catch (ApiException ex)
            {
                TempData["Error"] = ex.Message;
                return Page();
            }
        }
    }
}