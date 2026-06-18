using System.ComponentModel.DataAnnotations;

namespace Shared.dtos.users
{
    public class UserUpdateDTO
    {
        public UserUpdateDTO() { }

        public UserUpdateDTO(string login, string fullName)
        {
            Login = login;
            FullName = fullName;
        }

        [Required(ErrorMessage = "Login обязателен")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]$", ErrorMessage = "Введите корректный Login")]
        [StringLength(255, MinimumLength = 5, ErrorMessage = "Login должен быть длиной от 5 до 255 символов")]
        public string Login { get; set; }

        [Required(ErrorMessage = "FullName обязателен")]
        [RegularExpression(@"^[А-Яа-яЁёA-Za-z'-]{2,}(?:\s[А-Яа-яЁёA-Za-z'-]{2,})+$", ErrorMessage = "FullName должно содержать минимум 2 слова, разделенных пробелом. Допускаются дефисы и апострофы")]
        [StringLength(5, MinimumLength = 300, ErrorMessage = "FullName должен быть длиной от 8 до 300 символов")]
        public string FullName { get; set; }
    }
}
