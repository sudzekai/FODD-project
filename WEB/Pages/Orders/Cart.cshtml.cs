using Clients.webClients.statuses.interfaces;
using Clients.webClients.users.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shared.dtos.orders;
using Shared.types.exceptions;
using System.Security.Claims;

namespace WEB.Pages.Orders
{
    [Authorize]
    public class CartModel : PageModel
    {
        private readonly IUserOrdersWebClient _userOrdersClient;
        private readonly IStatusesWebClient _statusesClient;

        public CartModel(
            IUserOrdersWebClient userOrdersClient,
            IStatusesWebClient statusesClient)
        {
            _userOrdersClient = userOrdersClient;
            _statusesClient = statusesClient;
        }

        public OrderDTO? Cart { get; set; }

        public string StatusName { get; set; } = "";

        public async Task OnGetAsync(CancellationToken ct = default)
        {
            try
            {
                var userId = int.Parse(
                    HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

                var token = HttpContext.User.FindFirst("Token")?.Value;

                Cart = await _userOrdersClient
                    .GetCartOrderByUserIdAsync(userId, token, ct);

                if (Cart != null)
                {
                    try
                    {
                        var status = await _statusesClient
                            .GetStatusByIdAsync(Cart.StatusId, token, ct);

                        StatusName = status.Name;
                    }
                    catch (Exception ex)
                    {
                        TempData["Error"] = ex.Message;
                    }
                }
            }
            catch (ApiException ex)
            {
                if (ex.Code != 404)
                    TempData["Error"] = ex.Message;
            }
        }
    }
}