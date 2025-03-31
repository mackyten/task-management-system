using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace TMS.API.Configuration
{
    public class Swash
 {

        internal static void RegisterSwash(WebApplicationBuilder builder)
        {
            var aspEnv = builder.Configuration.GetSection("ASPNETCORE_ENVIRONMENT")?.Value;
            var clinetEnv = builder.Configuration.GetSection("ASPNETCORE_ENVIRONMENT")?.Value;

            if (clinetEnv == "Local" || aspEnv == "Development" || aspEnv == "Production" || aspEnv == "Test")
            {
                builder.Services.AddSwaggerGen(options =>
                {
                    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Description = "Copy 'Bearer ' + valid JWT token into field",
                        // Scheme = "Bearer",
                    });
                    options.OperationFilter<SecurityRequirementsOperationFilter>();
                    options.CustomSchemaIds(x => x.FullName);
                });
            }
        }

        internal static void ConfigureSwash(WebApplication app, WebApplicationBuilder builder)
        {
            var aspEnv = builder.Configuration.GetSection("ASPNETCORE_ENVIRONMENT")?.Value;

            if (app.Environment.IsDevelopment() || app.Environment.IsProduction() || aspEnv == "Local" || aspEnv == "Test")
            {
                app.UseSwagger(options =>
                {
                    options.SerializeAsV2 = true;
                });

                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                });
            }
        }

        internal static void UseSwagger(WebApplication app)
        {
            if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Local"))
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
        }
    }
}