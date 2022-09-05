using CursoRestWebApi.Api.Extensions;
using CursoRestWebApi.Data.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursoRestWebApi.Api.Configurations
{
    public static class IdentityConfiguration
    {
        public static IServiceCollection AddIdentityConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationIdentityDbContext>(opt =>
                opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
                .AddErrorDescriber<IdentityErrorMessagesPortuguese>()
                .AddDefaultTokenProviders();

            var section = configuration.GetSection("TokenSettings");
            services.Configure<TokenSettings>(section);

            var appSettings = section.Get<TokenSettings>();
            var secretKey = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwt =>
            {
                jwt.SaveToken = true;
                jwt.RequireHttpsMetadata = false;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = appSettings.Emissor,
                    ValidAudience = appSettings.ValidoEm
                };
            });

            return services;
        }
    }
}
