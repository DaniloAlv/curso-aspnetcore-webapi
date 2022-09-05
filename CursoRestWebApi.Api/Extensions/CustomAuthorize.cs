using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Security.Claims;

namespace CursoRestWebApi.Api.Extensions
{
    public class CustomAuthorization
    {
        public static bool ValidaClaimsUser(HttpContext context, string typeClaim, string valueClaim)
        {
            return context.User.Identity.IsAuthenticated &&
                context.User.Claims.Any(c => c.Type.Equals(typeClaim) && c.Value.Contains(valueClaim));
        }
    }

    public class RequisitoClaimFilter : IAuthorizationFilter
    {
        private readonly Claim _claim;

        public RequisitoClaimFilter(Claim claim)
        {
            _claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if(!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (!CustomAuthorization.ValidaClaimsUser(context.HttpContext, _claim.Type, _claim.Value))
                context.Result = new StatusCodeResult(403);
        }
    }

    public class ClaimsAuthorizeAttribute : TypeFilterAttribute
    {
        public ClaimsAuthorizeAttribute(string claimName, string claimValue) : base(typeof(RequisitoClaimFilter))
        {
            Arguments = new object[] { new Claim(claimName, claimValue) };
        }
    }
}
