
using DB.dbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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
