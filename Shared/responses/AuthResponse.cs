namespace Shared.responses
{
    public class AuthResponse
    {
        public AuthResponse()
        {
            
        }

        public AuthResponse(string role, string token, string fullName)
        {
            Role = role;
            Token = token;
            FullName = fullName;
        }

        public string FullName { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
