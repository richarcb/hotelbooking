namespace UserService.AuthService
{
    public interface IAuthService
    {
        string HashPassword(string password);
        bool VerifyPassword(string storedHash, string password);
        bool ValidateToken(string token);
        string GenerateToken(string username);

        

    }
}
