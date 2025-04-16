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
using TMS.APPLICATION.Services;


namespace TMS.INFRASTRUCTURE.Persistence.Repositories
{
    public class AppUserRepository : IAppUserRepository
    {

        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ITokenService tokenService;
        private readonly string AuthErrorMessage = "Login Failed. Please check your email or password";

        public AppUserRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITokenService tokenService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenService = tokenService;
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

        public async Task<AuthCredentialDTO> LoginUser(string email, string password)
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

            var result = new AuthCredentialDTO
            {
                Token = await tokenService.GenerateTokenAsync(user),
            };

            return result;
        }

        public async Task<ApplicationUser> GetByIdAsync(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            return user;
        }
    }
}