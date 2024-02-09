using MC.ApplicationServices.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MC.ApplicationServices.Implementations
{
    /// <summary>
    /// Authenticate by static data.
    /// </summary>
    public class JWTAuthenticationsManager : IJWTAuthenticationsManager
    {
        private readonly Dictionary<string, string> _clients = new()
        {
            { "fmi", "fmi" }
        };

        private readonly string tokenKey;

        public JWTAuthenticationsManager(string tokenKey)
        {
            this.tokenKey = tokenKey;
        }

        public string? Authenticate(string clientId, string secret)
        {
            if (!_clients.Any(x => x.Key == clientId && x.Value == secret))
            {
                return null;
            }

            JwtSecurityTokenHandler handler = new();
            var key = Encoding.ASCII.GetBytes(tokenKey);
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, clientId),
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            var token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);
        }
    }
}
