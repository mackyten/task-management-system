using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS.DOMAIN.DTOs;
using TMS.DOMAIN.Entities;

namespace TMS.DOMAIN.Interfaces
{
    public interface IAppUserRepository
    {
        Task<ApplicationUser?> CreateUser(ApplicationUser NewUser, string Password);
        Task<AuthCredentialDTO> LoginUser(string email, string password);
        Task<ApplicationUser> GetByIdAsync(string id);

    }
}