using Clients.webClients;
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
        private readonly IAuthWebClient _webClient;

        public LoginModel(IAuthWebClient webClient)
        {
            _webClient = webClient;
        }

        [BindProperty]
        public AuthRequest AuthRequest { get; set; } = new();

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                var response = await _webClient.LoginAsync(AuthRequest);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, response.FullName),
                    new Claim(ClaimTypes.Role, response.Role),
                };
                var identity = new ClaimsIdentity(claims, "Cookies");
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("Cookies", principal);

                WebClient.AddBearerToken(response.Token);

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
