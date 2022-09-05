using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;
using Swashbuckle.Swagger;
using IOperationFilter = Swashbuckle.AspNetCore.SwaggerGen.IOperationFilter;
using Microsoft.AspNetCore.Builder;

namespace CursoRestWebApi.Api.Extensions
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(opt =>
            {
                opt.OperationFilter<SwaggerDefaultValues>();

                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Insira o JWT neste formato: Bearer {seu token}",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { 
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()                    
                    }
                });
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger();
            app.UseSwaggerUI(opt =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    opt.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
            });

            return app;
        }
    }

    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        readonly IApiVersionDescriptionProvider _apiVersion;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider apiVersion)
        {
            _apiVersion = apiVersion;
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _apiVersion.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }

        static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo
            {
                Title = "WebApi para curso REST WebApi",
                Version = description.ApiVersion.ToString(),
                Description = "WebApi para o curso de REST WebApi",
                Contact = new OpenApiContact { Name = "Danilo Alves", Email = "dan.alves300@gmail.com" },
                TermsOfService = new Uri("https://opensource.org/licenses/MIT"),
                License = new OpenApiLicense { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
            };

            if (description.IsDeprecated)
                info.Description = string.Concat(info.Description, " - Essa versão está obsoleta!");

            return info;
        }
    }

    public class SwaggerDefaultValues : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var apiDescription = context.ApiDescription;

            operation.Deprecated = apiDescription.IsDeprecated();

            if (operation.Parameters == null) return;

            foreach (var parameter in operation.Parameters.OfType<NonBodyParameter>())
            {
                var description = apiDescription.ParameterDescriptions.First(p => p.Name == parameter.Name);

                if (parameter.Description == null)
                    parameter.Description = description.ModelMetadata?.Description;

                if (parameter.Default == null)
                    parameter.Default = description.DefaultValue;

                parameter.Required |= description.IsRequired;
            }
        }
    }

    public class NonBodyParameter
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public object Default { get; set; }
        public bool Required { get; set; }
    }
}
