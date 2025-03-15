using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TMS.APPLICATION.Common;
using TMS.APPLICATION.Common.Responses;
using TMS.APPLICATION.Features.DTOs;
using TMS.APPLICATION.Features.TaskItems.Command.Create;
using Command = TMS.APPLICATION.Features.TaskItems.Command;
using Queries = TMS.APPLICATION.Features.TaskItems.Queries;

namespace TMS.API.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TaskItemController : BaseController
    {

        /// <summary>
        /// Get a task by ID
        /// </summary>
        [HttpGet("{taskId}")]
        [ProducesResponseType(typeof(SuccessResponse<TaskItemResponseDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetTaskById([FromRoute] int taskId)
        {
            var query = new Queries.GetById.Query { TaskId = taskId };
            var result = await Mediator.Send(query);

            return result switch
            {
                NotFoundResponse notFound => NotFound(notFound),
                SuccessResponse<TaskItemResponseDTO> success => Ok(success),
                _ => StatusCode(500, new { Message = "An unexpected error occurred." })
            };
        }


        /// <summary>
        /// Create a new task
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(SuccessResponse<TaskItemResponseDTO>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(BadRequestResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateTask([FromBody] Command.Create.Command command)
        {
            var result = await Mediator.Send(command);

            return result switch
            {
                BadRequestResponse badRequest => BadRequest(badRequest),
                SuccessResponse<TaskItemResponseDTO> success => CreatedAtAction(nameof(GetTaskById), new { taskId = success.Data.Id }, success),
                _ => StatusCode(500, new { Message = "An unexpected error occurred." })
            };
        }

    }
}