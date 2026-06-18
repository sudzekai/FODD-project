
using DB.dbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Services.interfaces;
using Services.services;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ShoesStoreDbContext>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            builder.Services.AddScoped<IUsersService, UsersService>();

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            builder.Services.AddControllers(opt =>
            {
                opt.ModelBindingMessageProvider.SetValueIsInvalidAccessor(value =>
                    $"Значение '{value}' не является корректным."
                );

                opt.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(value =>
                    $"Значение '{value}' должно быть числом."
                );
            }).AddJsonOptions(opt =>
                {
                    opt.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
                    
                });
            
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
    }
}
