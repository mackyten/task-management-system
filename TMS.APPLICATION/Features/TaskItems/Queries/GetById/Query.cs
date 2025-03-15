using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TMS.APPLICATION.Common;
using TMS.APPLICATION.Common.Responses;
using TMS.APPLICATION.Features.DTOs;
using TMS.DOMAIN.Interfaces;

namespace TMS.APPLICATION.Features.TaskItems.Queries.GetById
{
    public class Query : IRequest<Response>
    {
        public int TaskId { get; set; }
    }

    public class QueryHandler : IRequestHandler<Query, Response>
    {
        private readonly ITaskItemRepository taskItemRepository;
        private readonly IMapper mapper;

        public QueryHandler(ITaskItemRepository taskItemRepository, IMapper mapper)
        {
            this.taskItemRepository = taskItemRepository;
            this.mapper = mapper;
        }

        public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
        {
            var taskItem = await taskItemRepository.GetById(request.TaskId);

            if (taskItem == null)
            {
                return new NotFoundResponse("Task not found");
            }

            var taskItemDto = mapper.Map<TaskItemResponseDTO>(taskItem);
            return new SuccessResponse<TaskItemResponseDTO>(taskItemDto);
        }
    }

}