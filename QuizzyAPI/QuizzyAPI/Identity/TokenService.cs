using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QuizzyAPI.Identity;

public class TokenService : ITokenService {
    private readonly UserManager<QuizzyUser> _userManager;
    private readonly IConfiguration _configuration;

    public TokenService(UserManager<QuizzyUser> userManager, IConfiguration configuration) {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<string> GetTokenAsync(string userName) {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtKey = Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]);

        var user = await _userManager.FindByNameAsync(userName);
        var roles = await _userManager.GetRolesAsync(user);
        var claims = new List<Claim> {
            new Claim(ClaimTypes.Name, userName)
        };

        foreach (var role in roles) {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor {
            Subject = new ClaimsIdentity(claims.ToArray()),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(jwtKey), SecurityAlgorithms.HmacSha256Signature)
        });

        return tokenHandler.WriteToken(token);
    }
}
