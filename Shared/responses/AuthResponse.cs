namespace Shared.responses
{
    public class AuthResponse
    {
        public AuthResponse()
        {
            
        }

        public AuthResponse(string role, string token, string fullName, int userId = 0)
        {
            Role = role;
            Token = token;
            FullName = fullName;
            UserId = userId;
        }

        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
