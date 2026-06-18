using System.ComponentModel.DataAnnotations;

namespace Shared.dtos.users
{
    public class UserPasswordUpdateDTO
    {
        public UserPasswordUpdateDTO() { }
        
        public UserPasswordUpdateDTO(string password)
        {
            Password = password;
        }


        [Required(ErrorMessage = "Password обязателен")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$", ErrorMessage = "Password должен содержать хотя бы одну заглавную, одну строчную букву и одну цифру")]
        [StringLength(25, MinimumLength = 8, ErrorMessage = "Password должен быть длиной от 8 до 25 символов")]
        public string Password { get; set; }
    }
}
