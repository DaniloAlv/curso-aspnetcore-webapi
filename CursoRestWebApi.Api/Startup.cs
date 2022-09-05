using CursoRestWebApi.Api.Extensions;
using CursoRestWebApi.Business.Interfaces.Repositorys;
using CursoRestWebApi.Data.Context;
using CursoRestWebApi.Data.Repositorys;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CursoRestWebApi.Api.Configurations;
using CursoRestWebApi.Business.Interfaces;
using Microsoft.AspNetCore.Http;
using CursoRestWebApi.Business.Interfaces.Services;
using CursoRestWebApi.Business.Services;
using CursoRestWebApi.Business.Notifications;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;

namespace CursoRestWebApi.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CursoRestWebApiDbContext>(option => 
                option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentityConfigurations(Configuration);

            services.AddAutoMapper(typeof(Startup).Assembly);

            services.AddApiVersioning(opt =>
            {
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(opt =>
            {
                opt.GroupNameFormat = "'v'VVV";
                opt.SubstituteApiVersionInUrl = true;
            });            

            services.Configure<ApiBehaviorOptions>(opt => 
            {
                opt.SuppressModelStateInvalidFilter = true;
            });

            services.AddCors(opt =>
            {
                opt.AddPolicy("Development", act =>
                    act.AllowAnyMethod()
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                );
            });

            services.AddControllers();

            services.AddSwaggerConfiguration();

            services.AddHealthChecks()
                .AddSqlServer(Configuration.GetConnectionString("DefaultConnection"), name: "BancoSQLServer");
            //services.AddHealthChecksUI();

            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IFornecedorRespository, FornecedorRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();

            services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<IFornecedorService, FornecedorService>();
            services.AddScoped<INotificador, Notificador>();

            services.AddScoped<IUser, UserLogged>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("Development");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwaggerConfiguration(provider);

            app.UseHealthChecks("/api/hc", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
            //app.UseHealthChecksUI(opt =>
            //{
            //    opt.UIPath = "/api/hc-ui";
            //});
        }
    }
}
