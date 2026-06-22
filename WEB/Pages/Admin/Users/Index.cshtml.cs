using Clients.webClients.users.interfaces;
using Clients.webClients.roles.interfaces; // если есть, иначе убрать
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shared.dtos.users;
using Shared.requests;

namespace WEB.Pages.Admin.Users
{
    public class IndexModel : PageModel
    {
        private readonly IUsersWebClient _users;

        public IndexModel(IUsersWebClient users)
        {
            _users = users;
        }

        public List<UserSimpleDTO> Users { get; set; } = new();

        public SelectList Roles { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public GetListRequest GetRequest { get; set; } = new();

        public async Task OnGetAsync()
        {
            var token = HttpContext.User.FindFirst("Token")?.Value;

            try
            {
                var usersTask = _users.GetUsersAsync(GetRequest, token);

                await Task.WhenAll(usersTask);

                Users = usersTask.Result;

                Roles = new SelectList(new List<object>(), "Id", "Name");
            }
            catch
            {
                Users = new List<UserSimpleDTO>();
                Roles = new SelectList(new List<object>(), "Id", "Name");
            }
        }
    }
}