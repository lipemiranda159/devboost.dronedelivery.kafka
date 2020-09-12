using devboost.dronedelivery.domain.Constants;
using devboost.dronedelivery.Extensions;
using devboost.dronedelivery.felipe.Security;
using devboost.dronedelivery.Infra.Data;
using devboost.dronedelivery.security.domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using System.Diagnostics.CodeAnalysis;

namespace devboost.dronedelivery
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    [ExcludeFromCodeCoverage]
    public class Startup
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        private const string SWAGGERFILE_PATH = "./swagger/v1/swagger.json";
        private const string LOCALHOST = "http://localhost:80";


#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public Startup(IConfiguration configuration)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            Configuration = configuration;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public IConfiguration Configuration { get; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public void ConfigureServices(IServiceCollection services)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            services.ConfigureCors();
            services.AddScopedServices(Configuration);
            services.AddAuthServices(Configuration);
            services.AddDbService();
            services.AddSwagger();

            services.AddCors();
            services.AddControllers()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseCors("CorsPolicy");

            // Criação de estruturas, usuários e permissões
            // na base do ASP.NET Identity Core (caso ainda não
            // existam)
            var validateDatabase = new ValidateDatabse();
            var droneRoleValidator = new DroneRoleValidator(roleManager);
            new IdentityInitializer(validateDatabase, userManager, droneRoleValidator).Initialize();

            // Swagger
            app.UseSwagger()
               .UseSwaggerUI(c =>
               {
                   c.RoutePrefix = string.Empty;
                   c.SwaggerEndpoint(SWAGGERFILE_PATH, ProjectConsts.PROJECT_NAME + ProjectConsts.API_VERSION);
               });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
