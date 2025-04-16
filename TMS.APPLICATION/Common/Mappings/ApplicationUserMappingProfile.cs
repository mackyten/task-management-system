using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TMS.APPLICATION.DTOs;
using TMS.APPLICATION.Features.ApplicationUsers.Commands.SignUp;
using TMS.DOMAIN.Entities;

namespace TMS.APPLICATION.Common.Mappings
{
    public class ApplicationUserMappingProfile : Profile
    {
        public ApplicationUserMappingProfile()
        {
            CreateMap<Command, ApplicationUser>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Important: Ignore the Id during creation
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email)) // Map Email to UserName
                                                                                        // Map other properties explicitly
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.MiddleName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Suffix, opt => opt.MapFrom(src => src.Suffix))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.Date)); // Ensure only the date part is mapped if needed
                                                                                                       // Add other mappings here if you have them
        }
    }
}