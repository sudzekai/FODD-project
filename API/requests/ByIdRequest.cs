using System.ComponentModel.DataAnnotations;

namespace API.requests
{
    public class ByIdRequest
    {
        [Required(ErrorMessage = "Id обязателен")]
        [Range(1, int.MaxValue, ErrorMessage = "Id должен быть больше 0")]
        public int Id { get; set; }
    }
}
