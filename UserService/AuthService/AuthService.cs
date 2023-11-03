using System.Security.Cryptography;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using BCrypt.Net;
using BookingService.Services;

namespace UserService.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly string _jwtSecretKey;
        private readonly IKeyVaultService _keyVaultService;

        public AuthService(IConfiguration configuration, IKeyVaultService keyVaultService)
        {
            _jwtSecretKey = _keyVaultService.GetSecret("JwtSecretKey");
            _keyVaultService = keyVaultService;
        }

        // When ValidateToken succeeds
        public string GenerateToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        // Used for registration
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        // When requesting an endpoint
        public bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecretKey);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                }, out SecurityToken validatedToken);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to validate token {ex.Message}");
                return false;
            }

        }

        // When 
        public bool VerifyPassword(string storedHash, string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, storedHash);
        }    

    }
}
