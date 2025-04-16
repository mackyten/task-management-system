using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using TMS.APPLICATION.Common.Responses;
using TMS.DOMAIN.Interfaces;

namespace TMS.APPLICATION.Features.TaskItems.Command.Delete
{
    public class Command : IRequest<Response>
    {
        public int Id { get; set; }
    }
    public class CommandHandler : IRequestHandler<Command, Response>
    {
        private readonly ITaskItemRepository taskItemRepository;

        public CommandHandler(ITaskItemRepository taskItemRepository)
        {
            this.taskItemRepository = taskItemRepository;
        }

        public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await taskItemRepository.DeleteAsync(request.Id);
                return new SuccessResponse<bool>(result);
            }
            catch(Exception e)
            {
                return new BadRequestResponse(e?.InnerException?.GetBaseException().Message ?? e.GetBaseException().Message);
            }
        }
    }

}