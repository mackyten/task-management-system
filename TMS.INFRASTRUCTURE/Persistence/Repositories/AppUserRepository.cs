using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS.DOMAIN.Entities;
using TMS.DOMAIN.Interfaces;
using TMS.DOMAIN.Entities;
using Microsoft.AspNetCore.Identity;


namespace TMS.INFRASTRUCTURE.Persistence.Repositories
{
    public class AppUserRepository : IAppUserRepository
    {

        private readonly UserManager<ApplicationUser> userManager;

        public AppUserRepository(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<ApplicationUser?> CreateUser(ApplicationUser NewUser, string Password)
        {

            var result = await userManager.CreateAsync(NewUser, Password);

            if (!result.Succeeded)
            {
                return null; // Return null if user creation fails
            }

            // Retrieve the newly created user with ID
            return await userManager.FindByEmailAsync(NewUser.Email!);
        }
    }
}