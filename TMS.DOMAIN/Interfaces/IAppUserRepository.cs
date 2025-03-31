using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS.DOMAIN.Entities;

namespace TMS.DOMAIN.Interfaces
{
    public interface IAppUserRepository
    {
        Task<ApplicationUser?> CreateUser(ApplicationUser NewUser, string Password);

    }
}