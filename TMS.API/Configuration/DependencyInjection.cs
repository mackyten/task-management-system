using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS.DOMAIN.Interfaces;
using TMS.INFRASTRUCTURE.Persistence.Repositories;

namespace TMS.API.Configuration
{
    public class DependencyInjection
    {

        internal static void RegisterDI(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ITaskItemRepository, TaskItemRepository>();
        }
    }
}