using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TMS.APPLICATION.Common;
using TMS.APPLICATION.Common.Responses;
using TMS.APPLICATION.DTOs;
using TMS.APPLICATION.Features.TaskItems.Command.Create;
using Command = TMS.APPLICATION.Features.TaskItems.Command;
using Queries = TMS.APPLICATION.Features.TaskItems.Queries;

namespace TMS.API.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    [Authorize]
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
        [HttpPost()]
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


        [HttpPut()]
        [ProducesResponseType((int)HttpStatusCode.NoContent)] // Indicate successful update with no content
        [ProducesResponseType(typeof(BadRequestResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)] // Consider adding NotFound if the resource doesn't exist
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateTask([FromBody] Command.Update.Command command)
        {
            var result = await Mediator.Send(command);

            return result switch
            {
                BadRequestResponse badRequest => BadRequest(badRequest),
                SuccessResponse<TaskItemResponseDTO> success => NoContent(),
                _ => StatusCode(500, new { Message = "An unexpected error occurred during task update." })
            };
        }

        [HttpDelete()]
        [ProducesResponseType((int)HttpStatusCode.NoContent)] // Indicate successful update with no content
        [ProducesResponseType(typeof(BadRequestResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)] // Consider adding NotFound if the resource doesn't exist
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteTask([FromBody] Command.Delete.Command command)
        {
            var result = await Mediator.Send(command);

            return result switch
            {
                BadRequestResponse badRequest => BadRequest(badRequest),
                SuccessResponse<bool> success => NoContent(),
                _ => StatusCode(500, new { Message = "An unexpected error occurred during task update." })
            };
        }

    }
}