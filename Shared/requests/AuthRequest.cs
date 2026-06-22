using System.ComponentModel.DataAnnotations;

namespace Shared.requests
{
    public class AuthRequest
    {
        public AuthRequest()
        {
            
        }

        public AuthRequest(string login, string password)
        {
            Login = login;
            Password = password;
        }

        [Required(ErrorMessage = "Login обязателен")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password обязателен")]
        public string Password { get; set; }
    }
}
