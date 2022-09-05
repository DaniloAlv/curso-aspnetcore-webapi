using System;
using System.Security.Claims;

namespace CursoRestWebApi.Api.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static string GetUserEmail(this ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal == null) throw new ArgumentNullException(nameof(claimsPrincipal));

            return claimsPrincipal.FindFirst(ClaimTypes.Email)?.Value;
        }

        public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal == null) throw new ArgumentNullException(nameof(claimsPrincipal));

            return claimsPrincipal.FindFirstValue("userId");
        }
    }
}
