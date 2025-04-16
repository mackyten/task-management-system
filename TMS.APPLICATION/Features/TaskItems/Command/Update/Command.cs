using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TMS.APPLICATION.Common.Responses;
using TMS.APPLICATION.DTOs;
using TMS.DOMAIN.Entities;
using TMS.DOMAIN.Enums;
using TMS.DOMAIN.Interfaces;

namespace TMS.APPLICATION.Features.TaskItems.Command.Update
{
    public class Command : IRequest<Response>
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public TaskItemStatus Status { get; set; }
        public DateTime? DueDate { get; set; }
        public TaskPriority Priority { get; set; }
        public string? AssignedToId { get; set; }
        public DateTime? DateFinished { get; set; }
    }

    public class CommandHandler : IRequestHandler<Command, Response>
    {
        private readonly ITaskItemRepository taskItemRepository;
        private readonly IMapper mapper;
        private readonly IAppUserRepository appUserRepository;

        public CommandHandler(ITaskItemRepository taskItemRepository, IMapper mapper, IAppUserRepository appUserRepository)
        {
            this.taskItemRepository = taskItemRepository;
            this.mapper = mapper;
            this.appUserRepository = appUserRepository;
        }

        public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
        {
            ApplicationUser? asignee = null;


            if (request.AssignedToId != null)
                asignee = await appUserRepository.GetByIdAsync(request.AssignedToId);

            // TaskItem task = mapper.Map<TaskItem>(request);
            TaskItem task = new TaskItem
            {
                Id = request.Id,
                Title = request.Title,
                Description = request.Description,
                Status = request.Status,
                DueDate = request.DueDate?.ToUniversalTime(),
                Priority = request.Priority,
                AssignedToId = asignee?.Id,
                AssignedTo = asignee,
                DateFinished = request.DateFinished?.ToUniversalTime(),
            };

            var updatedResult = await taskItemRepository.UpdateAsync(task);

            return new SuccessResponse<TaskItemResponseDTO>(mapper.Map<TaskItemResponseDTO>(updatedResult));
        }
    }
}