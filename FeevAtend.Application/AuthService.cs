using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using FeevAtend.Domain.Entities;

namespace FeevAtend.Application.Services;

public interface IAuthService
{
    string GenerateJwtToken(User user);
    User ValidateToken(string token);
}

public class AuthService : IAuthService
{
    private readonly string _jwtSecret;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserRepository _userRepository;

    public AuthService(string jwtSecret, IPasswordHasher passwordHasher, IUserRepository userRepository)
    {
        _jwtSecret = jwtSecret;
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
    }

    public string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "feevatend",
            audience: "feevatend",
            claims: claims,
            expires: DateTime.Now.AddDays(7),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public User ValidateToken(string token)
    {
        try
        {
            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadJwtToken(token);

            var userId = tokenS.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            return _userRepository.GetUserById(Guid.Parse(userId));
        }
        catch
        {
            return null;
        }
    }
}
