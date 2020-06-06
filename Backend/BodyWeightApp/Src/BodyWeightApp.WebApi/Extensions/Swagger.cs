using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace BodyWeightApp.WebApi.Extensions
{
    public static class Swagger
    {
        public static void WithSwaggerSecurity(this SwaggerGenOptions options)
        {
            var apiKeySchema = new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter JWT with Bearer into field",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            };
            options.AddSecurityDefinition("Bearer", apiKeySchema);

            var securityRequirements = new OpenApiSecurityRequirement
            {

                [new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,

                }] = new List<string>()

            };

            options.AddSecurityRequirement(securityRequirements);
        }
    }
}
