using System;
using System.IO;
using System.Linq;
using System.Reflection;

using BodyWeightApp.DataContext;
using BodyWeightApp.DataContext.DependencyInjection;
using BodyWeightApp.WebApi.Extensions;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using Newtonsoft.Json;

using Okta.AspNetCore;


namespace BodyWeightApp.WebApi
{
    public class Startup
    {
        private const string CorsPolicyName = "DefaultCorsPolicy";

        public Startup(IWebHostEnvironment env)
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appSettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = configBuilder.Build();

            var tokenProvider = new AzureServiceTokenProvider();
            var keyVaultClient = new KeyVaultClient((authority, resource, scope)
                => tokenProvider.KeyVaultTokenCallback(authority, resource, scope));

            configBuilder.AddAzureKeyVault(Configuration["KeyVault"], keyVaultClient, new DefaultKeyVaultSecretManager());

            Configuration = configBuilder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = OktaDefaults.ApiAuthenticationScheme;
                    options.DefaultChallengeScheme = OktaDefaults.ApiAuthenticationScheme;
                    options.DefaultSignInScheme = OktaDefaults.ApiAuthenticationScheme;
                })
                .AddOktaWebApi(new OktaWebApiOptions
                {
                    OktaDomain = Configuration["Okta:OktaDomain"],
                    AuthorizationServerId = Configuration["Okta:AuthorizationServerId"],
                    Audience = Configuration["Okta:Audience"]
                });
            services.AddControllers()
                .AddNewtonsoftJson(
                    options => options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc);

            services.AddSwaggerGen(c =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.SwaggerDoc("v1.0", new OpenApiInfo { Title = "Body Weight API", Version = "0.0.1" });
                c.WithSwaggerSecurity();
            });

            var origins = GetOrigins();
            services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicyName,
                    builder => builder.WithOrigins(origins)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                );
            });


            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.RegisterDataContextDependencies();

            services.AddDbContext<BodyInfoContext>(options =>
                options.UseNpgsql(Configuration["DbConnectionString"]), ServiceLifetime.Transient);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(ui =>
                {
                    ui.SwaggerEndpoint("/swagger/v1.0/swagger.json", "Body Weight API 0.0.1");
                    //To serve the Swagger UI at the application's root (http://localhost:<port>/),
                    //set the RoutePrefix property to an empty string
                    ui.RoutePrefix = string.Empty;
                });
            }

            app.UseCors(CorsPolicyName);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private string[] GetOrigins()
        {
            var originsEntry = Configuration["AllowedOrigins"]
                               ?? throw new InvalidOperationException("Missing AllowedOrigins in configuration");
            return originsEntry.Split(';', StringSplitOptions.RemoveEmptyEntries)
                .Select(value => value.Trim())
                .ToArray();
        }
    }
}
