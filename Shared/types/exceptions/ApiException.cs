namespace Shared.types.exceptions
{
    public class ApiException : Exception
    {
        public int? Code { get; }

        public ApiException(string message, int? code = null)
            : base(message)
        {
            Code = code;
        }
    }
}
