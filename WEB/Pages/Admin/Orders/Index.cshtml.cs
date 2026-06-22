using Clients.webClients.orders.interfaces;
using Clients.webClients.statuses.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shared.dtos.orders;
using Shared.requests;

namespace WEB.Pages.Admin.Orders
{
    public class IndexModel : PageModel
    {
        private readonly IOrdersWebClient _orders;
        private readonly IStatusesWebClient _statuses;

        public IndexModel(
            IOrdersWebClient orders,
            IStatusesWebClient statuses)
        {
            _orders = orders;
            _statuses = statuses;
        }

        public List<OrderSimpleDTO> Orders { get; set; } = new();

        public SelectList Statuses { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public GetOrdersListRequest GetRequest { get; set; } = new();

        public async Task OnGetAsync()
        {
            var token = HttpContext.User.FindFirst("Token")?.Value;

            try
            {
                var statusesTask = _statuses.GetStatusesAsync(token);
                var ordersTask = _orders.GetOrdersAsync(GetRequest, token);

                await Task.WhenAll(statusesTask, ordersTask);

                Statuses = new SelectList(statusesTask.Result, "Id", "Name");

                Orders = ordersTask.Result
                    .Select(x => new OrderSimpleDTO(
                        x.Id,
                        x.CreationDateTime,
                        x.DeliveryDate,
                        x.ReceiptCode,
                        x.StatusId,
                        x.UserId))
                    .ToList();
            }
            catch
            {
                Statuses = new SelectList(new List<object>(), "Id", "Name");
                Orders = new List<OrderSimpleDTO>();
            }
        }
    }
}