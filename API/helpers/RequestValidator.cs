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

            return errors;
        }
    }
}
