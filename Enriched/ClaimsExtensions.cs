using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Enriched.ClaimsExtended
{
    public static class ClaimsExtensions
    {
        public static IEnumerable<string> GetRoles(this ClaimsPrincipal claimsPrincipal)
        {
            var identity = claimsPrincipal?.Identity;
            return (identity as ClaimsIdentity)?.GetRoles();
        }

        public static IEnumerable<string> GetRoles(this ClaimsIdentity claimsIdentity)
        {
            if (claimsIdentity == null) return null;
            var claims = claimsIdentity.Claims;
            var roles = claims.Where(c => c.Type == ClaimTypes.Role);
            return roles.Select(x => x.Value);
        }

        public static string GetUserEmail(this ClaimsPrincipal claimsPrincipal)
        {
            var claim = claimsPrincipal?.FindFirst(ClaimTypes.Email);
            return claim?.Value;
        }

        public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            var claim = claimsPrincipal?.FindFirst(ClaimTypes.NameIdentifier);
            return claim?.Value;
        }

        public static string GetUserName(this ClaimsPrincipal claimsPrincipal)
        {
            var claim = claimsPrincipal?.FindFirst(ClaimTypes.Name);
            return claim?.Value;
        }

        public static bool IsAuthenticated(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal != null && claimsPrincipal.Identity != null && claimsPrincipal.Identity.IsAuthenticated;
        }
    }
}