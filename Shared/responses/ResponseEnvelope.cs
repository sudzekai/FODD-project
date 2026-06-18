using Shared.objects;

namespace Shared.responses
{
    public class ResponseEnvelope<T>
    {
        public ResponseEnvelope() {}
        public ResponseEnvelope(bool isSuccess, T data, Error? error) 
        {
            IsSuccess = isSuccess;
            Data = data;
            Error = error;
        }

        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
        public Error? Error { get; set; }
    }
}
