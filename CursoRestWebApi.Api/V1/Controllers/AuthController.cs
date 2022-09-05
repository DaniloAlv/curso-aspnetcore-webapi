using CursoRestWebApi.Api.Controllers;
using CursoRestWebApi.Api.DTOs;
using CursoRestWebApi.Api.Extensions;
using CursoRestWebApi.Business.Interfaces;
using CursoRestWebApi.Business.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CursoRestWebApi.Api.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly TokenSettings _tokenSettings;

        public AuthController(INotificador notificador,
                              SignInManager<IdentityUser> signInManager,
                              UserManager<IdentityUser> userManager,
                              IOptions<TokenSettings> tokenSettings,
                              IUser user) : base(notificador, user)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenSettings = tokenSettings.Value;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(RegisterDto user)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var userIdentity = new IdentityUser
            {
                Email = user.Email,
                UserName = user.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(userIdentity, user.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(userIdentity, false);
                return CustomResponse(null, await GerarJwt(userIdentity.Email));
            }

            foreach (var error in result.Errors)
            {
                NotificarErro(error.Description);
            }

            return CustomResponse();
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginDto>> Login(LoginDto user)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, false, true);

            if (result.Succeeded)
                return CustomResponse(null, await GerarJwt(user.Email));

            if (result.IsLockedOut)
            {
                NotificarErro("Seu usuário foi temporariamente bloqueado por tentativas excessivas de login. Por favor, volte a tentar mais tarde");
                return CustomResponse();
            }

            NotificarErro("Usuário e/ou senha inválido!");
            return CustomResponse();
        }

        private async Task<string> GerarJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim("userId", user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, email, ClaimValueTypes.Email));

            foreach (var role in userRoles)
            {
                claims.Add(new Claim("role", role));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_tokenSettings.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = identityClaims,
                Audience = _tokenSettings.ValidoEm,
                Issuer = _tokenSettings.Emissor,
                Expires = DateTime.UtcNow.AddHours(_tokenSettings.ExpiraHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            });

            string encodedToken = tokenHandler.WriteToken(token);
            return encodedToken;
        }
    }
}
