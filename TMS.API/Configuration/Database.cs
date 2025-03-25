using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using TMS.DOMAIN.Entities;
using TMS.INFRASTRUCTURE.Persistence;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace TMS.API.Configuration
{
    public class Database
    {
        internal static void RegisterDatabase(WebApplicationBuilder builder)
        {
            var jwtKey = builder.Configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(jwtKey))
            {
                throw new InvalidOperationException("JWT Key is missing in appsettings.json");
            }

            var key = Encoding.ASCII.GetBytes(jwtKey);

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add Identity Services
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            // Add Authentication and Authorization
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],  // Use Issuer from config
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["Jwt:Audience"],  // Use Audience from config
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };



                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async context =>
                    {
                        var userManager = context.HttpContext.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();
                        var userId = context.Principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                        if (userId == null)
                        {
                            context.Fail("Invalid Token: No User ID found.");
                            return;
                        }

                        var user = await userManager.FindByIdAsync(userId);
                        if (user == null)
                        {
                            context.Fail("Token is no longer valid: User not found.");
                            return;
                        }

                        // Check if the account is locked
                        if (user.LockoutEnd.HasValue && user.LockoutEnd > DateTime.UtcNow)
                        {
                            context.Fail("User account is locked.");
                            return;
                        }

                        // Validate SecurityStamp (prevents token reuse after password change)
                        var securityStampClaim = context.Principal?.FindFirst("SecurityStamp")?.Value;
                        if (securityStampClaim == null || user.SecurityStamp != securityStampClaim)
                        {
                            context.Fail("Token is no longer valid due to SecurityStamp mismatch.");
                        }
                    }
                };

            });
        }

        internal static void ApplyMigrations(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            dbContext.Database.Migrate();
        }
    }
}