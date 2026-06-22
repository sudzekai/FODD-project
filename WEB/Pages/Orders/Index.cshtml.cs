using Clients.webClients.orders.interfaces;
using Clients.webClients.statuses.interfaces;
using Clients.webClients.users.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shared.dtos.orders;
using Shared.dtos.statuses;
using Shared.requests;
using System.Security.Claims;


namespace WEB.Pages.Orders
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private string? Token => HttpContext.User.FindFirst("Token")?.Value;

        private readonly IOrdersWebClient _ordersClient;
        private readonly IStatusesWebClient _statusesWebClient;
        private readonly IUserOrdersWebClient _userOrdersWebClient;

        public IndexModel(IOrdersWebClient ordersClient, IStatusesWebClient statusesWebClient, IUserOrdersWebClient userOrdersWebClient)
        {
            _ordersClient = ordersClient;
            _statusesWebClient = statusesWebClient;
            _userOrdersWebClient = userOrdersWebClient;
        }

        [BindProperty(SupportsGet = true)]
        public GetOrdersListRequest GetRequest { get; set; } = new();

        public SelectList Statuses { get; set; } = default!;

        public List<OrderDTO> Orders { get; set; } = new();


        public async Task<IActionResult> OnGetAsync(bool isReset = false, CancellationToken ct = default)
        {
            if (isReset)
                return RedirectToPage();
            List<StatusDTO> statuses = [];

            try
            {
                statuses = await _statusesWebClient.GetStatusesAsync(Token);
                Statuses = new(statuses, "Id", "Name");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            if (HttpContext.User.FindFirst(ClaimTypes.Role).Value is var role && role is not null && role == "Клиент")
            {
                try
                {
                    var id = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
                    Orders = await _userOrdersWebClient.GetUserOrdersAsync(id, GetRequest, Token, ct);

                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                }
                return Page();
            }

            try
            {
                Orders = await _ordersClient.GetOrdersAsync(GetRequest, Token, ct);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return Page();
        }
    }
}