using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TMS.APPLICATION.Common.Responses;
using TMS.APPLICATION.DTOs;
using TMS.APPLICATION.Services;
using TMS.DOMAIN.DTOs;
using TMS.DOMAIN.Entities;
using TMS.DOMAIN.Interfaces;

namespace TMS.APPLICATION.Features.ApplicationUsers.Commands.SignUp
{
    public class Command : IRequest<Response>
    {
        //public Guid Id { get; set; } = Guid.NewGuid();
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public required string LastName { get; set; }
        public string? Suffix { get; set; }
        public DateTime DateOfBirth { get; set; }
    }

    public class CommandHandler : IRequestHandler<Command, Response>
    {
        private readonly IAppUserRepository appUserRepository;
        private readonly ITokenService tokenService;
        private readonly IMapper mapper;

        public CommandHandler(IAppUserRepository appUserRepository, ITokenService tokenService, IMapper mapper)
        {
            this.appUserRepository = appUserRepository;
            this.tokenService = tokenService;
            this.mapper = mapper;
        }

        public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
        {
            var response = new AuthCredentialDTO();
            var user = mapper.Map<ApplicationUser>(request);

            var newUser = await appUserRepository.CreateUser(user, request.Password) ?? throw new Exception();
            if (newUser != null)
            {
                response = await appUserRepository.LoginUser(request.Email, request.Password);
            }

            return new SuccessResponse<AuthCredentialDTO>(response);
        }
    }
}