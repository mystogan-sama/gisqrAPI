using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using gisAPI.Interfaces;
using gisAPI.Persistence.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace gisAPI.Security
{
    public class JwtGenerator : IJwtGenerator
    {
        private readonly SymmetricSecurityKey _key;

        public JwtGenerator(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }

        public string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
              new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
              new Claim(JwtRegisteredClaimNames.Name, user.Username),
              new Claim(ClaimTypes.Role, user.Role),
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = creds
            };

            var handler = new JwtSecurityTokenHandler();

            var token = handler.CreateToken(descriptor);

            return handler.WriteToken(token);
        }
    }
}