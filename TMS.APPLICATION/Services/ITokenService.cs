using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS.DOMAIN.Entities;

namespace TMS.APPLICATION.Services
{
    public interface ITokenService
    {
        Task<string> GenerateTokenAsync(ApplicationUser user);
    }
}