using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TMS.DOMAIN.Entities;

namespace TMS.APPLICATION.Common.Mappings
{
    public class TaskItemMappingProfile : Profile
    {
        public TaskItemMappingProfile()
        {
            CreateMap<TaskItem, TaskItemMappingProfile>(); // Maps TaskEntity → TaskResponseDto

        }
    }
}