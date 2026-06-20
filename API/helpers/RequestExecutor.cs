using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace API.helpers
{
    public static class RequestExecutor
    {
        public static async Task<IActionResult> Execute(Func<Task<IActionResult>> func, ModelStateDictionary modelState)
        {
            var errors = RequestValidator.ValidateModel(modelState);
            if (errors.Count > 0)
                return ResponseBuilder.BuildBadRequestErrors(errors);

            try
            {
                return await func();
            }
            catch (Exception ex)
            {
                var error = ExceptionHandler.Handle(ex);
                return ResponseBuilder.BuildError(error);
            }
        }
    }
}
