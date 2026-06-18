using System.ComponentModel.DataAnnotations;

namespace API.requests
{
    public class IdRequest
    {
        [Required(ErrorMessage = "Id обязателен")]
        [RegularExpression(@"^(?!-)(?!0+$)\d{1,9}$", ErrorMessage = "Id должен быть числом больше нуля")]
        public string Id { get; set; }

        public int GetId() => int.Parse(Id);
    }
}
