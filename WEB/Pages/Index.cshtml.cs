using Clients.webClients.users.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shared.dtos.users;
using Shared.requests;

namespace WEB.Pages
{
    public class IndexModel : PageModel
    {
        public List<UserSimpleDTO> Users { get; set; } = [];

        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }


        private readonly IUsersWebClient _webClient;

        public IndexModel(IUsersWebClient webClient)
        {
            _webClient = webClient;
        }

        public async Task OnGetAsync(string? search)
        {
            Search = search;

            var req = new GetListRequest
            {
                SearchTerm = search ?? ""
            };

            Users = await _webClient.GetUsersAsync(req);
        }
    }
}