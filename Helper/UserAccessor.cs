// using System.Linq;
// using System.Security.Claims;
// using Microsoft.AspNetCore.Http;

// namespace gisAPI.Helper
// {
//   public interface IUserAccessor
//   {
//     string GetCurrentUserId();
//     string GetCurrentUserRole();
//   }

//   public class UserAccessor : IUserAccessor
//   {
//     private readonly IHttpContextAccessor _httpContextAccessor;

//     public UserAccessor(IHttpContextAccessor httpContextAccessor)
//     {
//       _httpContextAccessor = httpContextAccessor;
//     }

//     public string GetCurrentUserId()
//     {
//       var username = _httpContextAccessor.HttpContext.User?.Claims?
//         .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

//       return username;
//     }

//     public string GetCurrentUserRole()
//     {
//       var role = _httpContextAccessor.HttpContext.User?.Claims?
//         .FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;

//       return role;
//     }

//   }
// }