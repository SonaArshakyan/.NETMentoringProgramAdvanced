using System.IdentityModel.Tokens.Jwt;

namespace MvcLoginClient.Models;

public class TokenInfoViewModel
{
    public string RefreshToken { get; set; }
    public JwtSecurityToken IdToken { get; set; }
    public JwtSecurityToken AccessToken { get; set; }
}
