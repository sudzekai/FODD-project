using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Shared.dtos.users
{
    public class UserCreateDTO
    {
        public UserCreateDTO() { }

        public UserCreateDTO(string login, string password, string fullName)
        {
            Login = login;
            Password = password;
            FullName = fullName;
        }

        [Required(ErrorMessage = "Login обязателен")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]+$", ErrorMessage = "Введите корректный Login")]
        [StringLength(255, MinimumLength = 5, ErrorMessage = "Login должен быть длиной от 5 до 255 символов")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password обязателен")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$", ErrorMessage = "Password должен содержать хотя бы одну заглавную, одну строчную букву и одну цифру")]
        [StringLength(25, MinimumLength = 8, ErrorMessage = "Password должен быть длиной от 8 до 25 символов")]
        public string Password { get; set; }


        [Required(ErrorMessage = "FullName обязателен")]
        [RegularExpression(@"^[А-Яа-яЁёA-Za-z'-]{2,}(?:\s[А-Яа-яЁёA-Za-z'-]{2,})+$", ErrorMessage = "FullName должно содержать минимум 2 слова, разделенных пробелом. Допускаются дефисы и апострофы")]
        [StringLength(300, MinimumLength = 5, ErrorMessage = "FullName должен быть длиной от 8 до 300 символов")]
        public string FullName { get; set; }
    }
}
