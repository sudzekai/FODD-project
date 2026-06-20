using Clients.webClients;
using Clients.webClients.manufacturers.clients;
using Clients.webClients.manufacturers.interfaces;
using Clients.webClients.orders.clients;
using Clients.webClients.orders.interfaces;
using Clients.webClients.products.clients;
using Clients.webClients.products.interfaces;
using Clients.webClients.roles.clients;
using Clients.webClients.roles.interfaces;
using Clients.webClients.statuses.clients;
using Clients.webClients.statuses.interfaces;
using Clients.webClients.suppliers.clients;
using Clients.webClients.suppliers.interfaces;
using Clients.webClients.tags.clients;
using Clients.webClients.tags.interfaces;
using Clients.webClients.users.clients;
using Clients.webClients.users.interfaces;

namespace WEB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            WebClient.SetBaseUrl("http://localhost:8080");

            AddWebClients(builder);

            builder.Services.AddRazorPages()
                    .AddRazorRuntimeCompilation();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapRazorPages()
               .WithStaticAssets();

            app.Run();
        }

        private static void AddWebClients(WebApplicationBuilder builder)
        {
            AddManufacturersWebClients(builder);
            AddOrdersWebClients(builder);
            AddProductsWebClients(builder);
            AddRolesWebClients(builder);
            AddStatusesWebClients(builder);
            AddSuppliersWebClients(builder);
            AddTagsWebClients(builder);
            AddUsersWebClients(builder);
        }

        private static void AddManufacturersWebClients(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IManufacturersWebClient, ManufacturersWebClient>();
        }

        private static void AddProductsWebClients(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IProductsWebClient, ProductsWebClient>();
            builder.Services.AddScoped<IProductTagsWebClient, ProductTagsWebClient>();
        }

        private static void AddOrdersWebClients(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IOrdersWebClient, OrdersWebClient>();
            builder.Services.AddScoped<IOrderProductsWebClient, OrderProductsWebClient>();
        }

        private static void AddRolesWebClients(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IRolesWebClient, RolesWebClient>();
        }

        private static void AddStatusesWebClients(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IStatusesWebClient, StatusesWebClient>();
        }

        private static void AddSuppliersWebClients(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ISuppliersWebClient, SuppliersWebClient>();
        }

        private static void AddTagsWebClients(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ITagsWebClient, TagsWebClient>();
        }

        private static void AddUsersWebClients(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUsersWebClient, UsersWebClient>();
            builder.Services.AddScoped<IUserOrdersWebClient, UserOrdersWebClient>();
        }
    }
}
