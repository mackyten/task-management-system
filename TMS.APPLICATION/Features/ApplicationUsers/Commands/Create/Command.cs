using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TMS.APPLICATION.Common.Responses;
using TMS.APPLICATION.DTOs;
using TMS.DOMAIN.Entities;
using TMS.DOMAIN.Interfaces;

namespace TMS.APPLICATION.Features.ApplicationUsers.Commands.Create
{
    public class Command : IRequest<Response>
    {
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
        private readonly IMapper mapper;

        public CommandHandler(IAppUserRepository appUserRepository, IMapper mapper)
        {
            this.appUserRepository = appUserRepository;
            this.mapper = mapper;
        }

        public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
        {
            try
            {
                var user = new ApplicationUser
                {
                    Email = request.Email,
                    FirstName = request.FirstName,
                    MiddleName = request.MiddleName,
                    LastName = request.LastName,
                    Suffix = request.Suffix,
                    DateOfBirth = request.DateOfBirth.Date
                };

                //mapper.Map<ApplicationUser>(request);
                var newUser = await appUserRepository.CreateUser(user, request.Password);
                var response = mapper.Map<AppUserDTO>(newUser);

                return new SuccessResponse<AppUserDTO>(response);
            }
            catch (Exception e)
            {
                return new BadRequestResponse("An unexpected error occured.");
            }
        }
    }
}