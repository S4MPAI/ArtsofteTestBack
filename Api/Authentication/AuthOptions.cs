using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Api.Authentication;

public class AuthOptions
{
    public string ISSUER { get; set; }
    public string AUDIENCE { get; set; }
    public string KEY { get; set; }
    public SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
}
