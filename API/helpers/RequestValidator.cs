using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Server.HttpSys;
using System.Text.RegularExpressions;

namespace API.helpers
{
    public class RequestValidator
    {
        public static List<string> ValidateModel(ModelStateDictionary model)
        {
            if (model.IsValid)
                return [];


            var errors = model
                    .Where(e => e.Value.Errors.Count > 0)
                    .SelectMany(e => e.Value.Errors.Select(err => err.ErrorMessage))
                    .ToList();

            errors = [.. errors.Select(e =>
            {
                if (e.Contains("JSON"))
                    return "Переданные данные имеют неверный формат";

                if (e.Contains("non-empty request body"))
                    return "Тело запроса обязательно";

                return e;
            }).Distinct()];

            return errors;
        }

        public static List<string> ValidateAsId(string idStr)
        {

            List<string> result = [];

            if (!int.TryParse(idStr, out int id))
            {
                result.Add("Id должен быть числом");
                return result;
            }

            if (id < 1)
                result.Add("Id должен быть > 0");

            return result;
        }
    }
}
