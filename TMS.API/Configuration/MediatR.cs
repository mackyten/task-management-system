using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TMS.APPLICATION.Common;
using TMS.APPLICATION.Common.Mappings;
using TMS.APPLICATION.Common.Responses;

namespace TMS.API.Configuration
{
    public class MediatR
    {
        internal static void RegisterMediatR(WebApplicationBuilder builder)
        {
            // builder.Services
            //     .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationAssemblyReference).Assembly));
            builder.Services.AddMediatR(typeof(ApplicationAssemblyReference).Assembly);
        }

        internal static void RegisterAutomapper(WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(typeof(ApplicationUserMappingProfile).Assembly);
        }
    }
}