using Shared.objects;
using Shared.types.exceptions;

namespace API.helpers
{
    public static class ExceptionHandler
    {
        public static Error Handle(Exception ex)
            => ex switch
            {
                BadRequestException => new(400, ex.Message),
                NotFoundException => new(404, ex.Message),
                ConflictException => new(409, ex.Message),
                InternalServerException => new(500, ex.Message),
                _ => new(500, $"Возникла непредвиденная ошибка при выполнении запроса: {ex}"),
            };
    }
}
