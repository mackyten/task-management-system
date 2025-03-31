using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TMS.APPLICATION.DTOs;
using TMS.DOMAIN.Entities;

namespace TMS.APPLICATION.Common.Mappings
{
    public class ApplicationUserMappingProfile : Profile
    {
        // public ApplicationUserMappingProfile()
        // {
        //     CreateMap<AppUserDTO, ApplicationUser>()
        //         .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email)); // Example mapping
        // }
    }
}