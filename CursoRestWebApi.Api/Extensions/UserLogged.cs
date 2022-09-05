using CursoRestWebApi.Business.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CursoRestWebApi.Api.Extensions
{
    public class UserLogged : IUser
    {
        private readonly IHttpContextAccessor _acessor;

        public UserLogged(IHttpContextAccessor acessor)
        {
            _acessor = acessor;
        }

        public string Name => _acessor.HttpContext.User.Identity.Name;

        public IEnumerable<Claim> GetUserClaims()
        {
            return _acessor.HttpContext.User.Claims;
        }

        public string GetUserEmail()
        {
            return IsAuthenticated() ? _acessor.HttpContext.User.GetUserEmail() : "";
        }

        public Guid GetUserId()
        {
            return IsAuthenticated() ? Guid.Parse(_acessor.HttpContext.User.GetUserId()) : Guid.Empty;
        }

        public bool IsAuthenticated()
        {
            return _acessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public bool IsInRole(string role)
        {
            return _acessor.HttpContext.User.IsInRole(role);
        }
    }
}
