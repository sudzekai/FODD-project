using Microsoft.AspNetCore.Mvc;
using Shared.objects;
using Shared.responses;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace API.helpers
{
    public static class ResponseBuilder
    {
        public static ObjectResult BuildError(Shared.objects.Error error)
        {
            var result = new ResponseEnvelope<object>
            {
                IsSuccess = false,
                Error = error
            };

            return new ObjectResult(result)
            {
                StatusCode = error.Code
            };
        }

        public static ObjectResult BuildBadRequestErrors(List<string> errors)
        {
            var result = new ResponseEnvelope<object>
            {
                IsSuccess = false,
                Error = new(400, string.Join(", ", errors))
            };

            return new ObjectResult(result)
            {
                StatusCode = 400
            };
        }

        public static ObjectResult BuildOk<T>(T data)
        {
            var result = new ResponseEnvelope<T>
            {
                IsSuccess = true,
                Data = data
            };

            return new ObjectResult(result)
            {
                StatusCode = 200
            };
        }

        public static ObjectResult BuildCreated<T>(T data)
        {
            var result = new ResponseEnvelope<object>
            {
                IsSuccess = true,
                Data = data
            };

            return new ObjectResult(result)
            {
                StatusCode = 201
            };
        }
    }
}
