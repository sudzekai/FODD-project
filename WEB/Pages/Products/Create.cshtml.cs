using Clients.webClients.products.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shared.dtos.products;

namespace WEB.Pages.Products
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public ProductCreateDTO Product { get; set; } = new();

        private readonly IProductsWebClient _webClient;

        public CreateModel(IProductsWebClient webClient)
        {
            _webClient = webClient;
        }

        public void OnGet()
        {

        }
    }
}
