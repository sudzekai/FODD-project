namespace API.utilities
{
    public interface IJwtTokenService
    {
        string GenerateToken(string userId, string role, string fullName);
    }
}