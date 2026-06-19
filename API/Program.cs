
using DB.dbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Services.manufacturers.interfaces;
using Services.manufacturers.services;
using Services.orders.interfaces;
using Services.orders.services;
using Services.products.interfaces;
using Services.products.services;
using Services.roles.interfaces;
using Services.roles.services;
using Services.statuses.interfaces;
using Services.statuses.services;
using Services.suppliers.interfaces;
using Services.suppliers.services;
using Services.tags.interfaces;
using Services.tags.services;
using Services.unitOfWork;
using Services.users.interfaces;
using Services.users.services;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            builder.Services.AddDbContext<ShoesStoreDbContext>();

            AddServices(builder);

            AddControllesWithOptions(builder);

            builder.Services.AddOpenApi();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        private static void AddServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            AddManufacturersServices(builder);
            AddOrdersServices(builder);
            AddProductsServices(builder);
            AddRolesServices(builder);
            AddStatusesServices(builder);
            AddSuppliersServices(builder);
            AddTagsServices(builder);
            AddUsersServices(builder);
        }

        private static void AddManufacturersServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IManufacturersService, ManufacturersService>();
        }
        private static void AddUsersServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUsersService, UsersService>();
            builder.Services.AddScoped<IUserOrdersService, UserOrdersService>();
        }

        private static void AddProductsServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IProductsService, ProductsService>();
            builder.Services.AddScoped<IProductTagsService, ProductTagsService>();
        }

        private static void AddOrdersServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IOrdersService, OrdersService>();
            builder.Services.AddScoped<IOrderProductsService, OrderProductsService>();
        }

        private static void AddRolesServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IRolesService, RolesService>();
        }
        
        private static void AddStatusesServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IStatusesService, StatusesService>();
        }

        private static void AddSuppliersServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ISuppliersService, SuppliersService>();
        }
        
        private static void AddTagsServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ITagsService, TagsService>();
        }

        private static void AddControllesWithOptions(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers(opt =>
            {
                opt.ModelBindingMessageProvider.SetValueIsInvalidAccessor(value =>
                    $"Значение '{value}' некорректно."
                );

                opt.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(value =>
                    $"Значение '{value}' должно быть числом."
                );

                opt.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(_ =>
                    "Значение не должно быть null."
                );

                opt.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((value, fieldName) =>
                    $"Значение '{value}' некорректно для поля '{fieldName}'."
                );

                opt.ModelBindingMessageProvider.SetUnknownValueIsInvalidAccessor(fieldName =>
                    $"Указано некорректное значение для поля '{fieldName}'."
                );

                opt.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor(fieldName =>
                    $"Поле '{fieldName}' является обязательным."
                );

                opt.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(() =>
                    "Отсутствует ключ или значение."
                );

                opt.ModelBindingMessageProvider.SetMissingRequestBodyRequiredValueAccessor(() =>
                    "Тело запроса обязательно."
                );

                opt.ModelBindingMessageProvider.SetNonPropertyAttemptedValueIsInvalidAccessor(value =>
                    $"Значение '{value}' некорректно."
                );

                opt.ModelBindingMessageProvider.SetNonPropertyUnknownValueIsInvalidAccessor(() =>
                    "Указано некорректное значение."
                );

                opt.ModelBindingMessageProvider.SetNonPropertyValueMustBeANumberAccessor(() =>
                    "Значение должно быть числом."
                );

                opt.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(value =>
                    $"Значение '{value}' должно быть числом."
                );

            }).AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;

            });
        }
    }
}
