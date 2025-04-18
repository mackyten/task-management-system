using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS.APPLICATION.Services;
using TMS.DOMAIN.Interfaces;
using TMS.INFRASTRUCTURE.Persistence.Repositories;
using TMS.INFRASTRUCTURE.Persistence.Services;

namespace TMS.API.Configuration
{
    public class DependencyInjection
    {

        internal static void RegisterDI(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ITaskItemRepository, TaskItemRepository>();
            builder.Services.AddScoped<IAppUserRepository, AppUserRepository>();
            builder.Services.AddScoped<ITokenService, TokenService>();
        }
    }
}