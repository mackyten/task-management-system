using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using TMS.APPLICATION.Common.Responses;
using TMS.APPLICATION.DTOs;
using TMS.DOMAIN.DTOs;
using Commands = TMS.APPLICATION.Features.ApplicationUsers.Commands;
using Queries = TMS.APPLICATION.Features.ApplicationUsers.Queries;

namespace TMS.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class ApplicationUserController : BaseController
    {

        /// <summary>
        /// Get a user by Id
        /// </summary>
        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(SuccessResponse<AppUserDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetByUserId([FromRoute] string userId)
        {
            var query = new Queries.GetById.Query { Id = userId };
            var result = await Mediator.Send(query);

            return result switch
            {
                NotFoundResponse notFound => NotFound(notFound),
                SuccessResponse<AppUserDTO> success => Ok(success),
                _ => StatusCode(500, new { Message = "An unexpected error occurred." })
            };
        }


        /// <summary>
        /// Creates a new user
        /// </summary>
        [HttpPost()]
        [ProducesResponseType(typeof(SuccessResponse<AppUserDTO>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(BadRequestResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateUser([FromBody] Commands.Create.Command command)
        {
            var result = await Mediator.Send(command);


            return result switch
            {
                BadRequestResponse badRequest => BadRequest(badRequest),
                SuccessResponse<AppUserDTO> success => CreatedAtAction(nameof(GetByUserId), new { userId = success.Data.Id }, success),
                _ => StatusCode(500, new { Message = "An unexpected error occurred." })
            };
        }


        /// <summary>
        /// Signs up a new user, will return a token
        /// </summary>
        [HttpPost("signup")]
        [ProducesResponseType(typeof(SuccessResponse<AuthCredentialDTO>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(BadRequestResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)] // Add for unexpected errors
        public async Task<IActionResult> SignUp([FromBody] Commands.SignUp.Command command)
        {
            var result = await Mediator.Send(command);

            return result switch
            {
                BadRequestResponse badRequest => BadRequest(badRequest),
                SuccessResponse<AuthCredentialDTO> success => CreatedAtAction(null, success.Data), // Return 201 with the DTO in the body
                _ => StatusCode((int)HttpStatusCode.InternalServerError, new { Message = "An unexpected error occurred during sign-up." })
            };
        }


        /// <summary>
        /// Signs in a user, will return a token
        /// </summary>
        [HttpPost("signin")]
        [ProducesResponseType(typeof(SuccessResponse<AuthCredentialDTO>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(BadRequestResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)] // Add for unexpected errors
        public async Task<IActionResult> SigIn([FromBody] Commands.SignIn.Command command)
        {
            var result = await Mediator.Send(command);

            return result switch
            {
                BadRequestResponse badRequest => BadRequest(badRequest),
                SuccessResponse<AuthCredentialDTO> success => CreatedAtAction(null, success.Data), // Return 201 with the DTO in the body
                _ => StatusCode((int)HttpStatusCode.InternalServerError, new { Message = "An unexpected error occurred during sign-up." })
            };
        }


    }
}