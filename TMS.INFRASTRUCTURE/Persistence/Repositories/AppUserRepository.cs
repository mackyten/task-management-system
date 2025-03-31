using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS.DOMAIN.Entities;
using TMS.DOMAIN.Interfaces;
using TMS.DOMAIN.Entities;
using Microsoft.AspNetCore.Identity;
using TMS.APPLICATION.DTOs;
using TMS.DOMAIN.DTOs;


namespace TMS.INFRASTRUCTURE.Persistence.Repositories
{
    public class AppUserRepository : IAppUserRepository
    {

        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly string AuthErrorMessage = "Login Failed. Please check your email or password";

        public AppUserRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<ApplicationUser?> CreateUser(ApplicationUser NewUser, string Password)
        {

            var result = await userManager.CreateAsync(NewUser, Password);

            if (!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(e => $"{e.Code}: {e.Description}"));
                throw new InvalidOperationException($"User creation failed: {errors}");
            }

            // Retrieve the newly created user with ID
            return await userManager.FindByEmailAsync(NewUser.Email!);
        }

        public async Task<AuthenticatedUserDTO> LoginUser(string email, string password)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new Exception(AuthErrorMessage);
            }
            var signin = await signInManager.PasswordSignInAsync(user, password, isPersistent: false, lockoutOnFailure: true);

            if (!signin.Succeeded)
            {
                throw new Exception(AuthErrorMessage);
            }

            var result = new AuthenticatedUserDTO();

            return result;
        }
    }
}