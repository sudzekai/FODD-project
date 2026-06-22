using Clients.webClients.users.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shared.dtos.users;
using Shared.types.exceptions;

namespace WEB.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IUsersWebClient _usersWebClient;

        public RegisterModel(IUsersWebClient usersWebClient)
        {
            _usersWebClient = usersWebClient;
        }

        [BindProperty]
        public UserCreateDTO UserCreateDTO { get; set; } = new();

        public void OnGet() { }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            try
            {
                await _usersWebClient.CreateUserAsync(UserCreateDTO, token: null);
                return RedirectToPage("/Login");
            }
            catch (ApiException ex)
            {
                TempData["Error"] = ex.Message;
                return Page();
            }
        }
    }
}