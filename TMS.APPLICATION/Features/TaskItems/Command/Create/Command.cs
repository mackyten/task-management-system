using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TMS.APPLICATION.Common;
using TMS.APPLICATION.Common.Responses;
using TMS.APPLICATION.Features.DTOs;
using TMS.DOMAIN.Entities;
using TMS.DOMAIN.Enums;
using TMS.DOMAIN.Interfaces;

namespace TMS.APPLICATION.Features.TaskItems.Command.Create
{
    public class Command : IRequest<Response>
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
    }

    public class CommmandHandler : IRequestHandler<Command, Response>
    {
        private readonly ITaskItemRepository taskItemRepository;
        private readonly IMapper mapper;

        public CommmandHandler(ITaskItemRepository taskItemRepository, IMapper mapper)
        {
            this.taskItemRepository = taskItemRepository;
            this.mapper = mapper;
        }

        public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
        {
            var newTask = new TaskItem
            {
                Title = request.Title,
                Description = request.Description,
                Status = TaskItemStatus.New,
            };



            var result = await taskItemRepository.AddAsync(newTask);

            if (result == null)
                return new BadRequestResponse("Task not found");

            var response = mapper.Map<TaskItemResponseDTO>(result);


           return new SuccessResponse<TaskItemResponseDTO>(response);

        }
    }
}