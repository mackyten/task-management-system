using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using TMS.APPLICATION.Common.Responses;
using TMS.DOMAIN.DTOs;
using TMS.DOMAIN.Interfaces;

namespace TMS.APPLICATION.Features.ApplicationUsers.Commands.SignIn
{
    public class Command : IRequest<Response>
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }

    public class CommandHandler : IRequestHandler<Command, Response>
    {
        private readonly IAppUserRepository appUserRepository;

        public CommandHandler(IAppUserRepository appUserRepository)
        {
            this.appUserRepository = appUserRepository;
        }

        public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
        {

            var response = await appUserRepository.LoginUser(request.Email, request.Password);
            return new SuccessResponse<AuthCredentialDTO>(response);
        }
    }
}