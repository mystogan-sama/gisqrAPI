using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using gisAPI.Interfaces;

namespace gisAPI.Security
{
    public class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }


        public string GetCurrentUsername()
        {
            var username = _httpContextAccessor.HttpContext.User?.Claims
              ?.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Name)?.Value;

            return username;
        }

        public string GetCurrentRole()
        {
            var role = _httpContextAccessor.HttpContext.User?.Claims
              ?.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;

            return role;
        }
    }
}