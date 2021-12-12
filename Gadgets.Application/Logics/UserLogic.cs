using Gadgets.Application.Configurations;
using Gadgets.Application.Contracts.Authentication;
using Gadgets.Application.Contracts.Data;
using Gadgets.Application.Contracts.Logics;
using Gadgets.Application.Models.InputModels;
using Gadgets.Application.Models.ViewModels;
using Gadgets.Application.Services;
using Gadgets.Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Gadgets.Application.Logics
{
    public class UserLogic : IUserLogic
    {
        public UserLogic
            (
                IHttpContextAccessor context,
                IApplicationDbContext database, ITokenService tokenService, IOptions<JwtConfiguration> configuration,
                UserManager<User> userManager, SignInManager<User> signInManager, ILogger<UserLogic> logger
            )
        {
            Context = context;
            Database = database;
            TokenService = tokenService;
            Configuration = configuration.Value;
            UserManager = userManager;
            SignInManager = signInManager;
            Logger = logger;
        }

        readonly IHttpContextAccessor Context;
        readonly IApplicationDbContext Database;
        readonly ITokenService TokenService;
        readonly JwtConfiguration Configuration;
        readonly UserManager<User> UserManager;
        readonly SignInManager<User> SignInManager;
        readonly ILogger<UserLogic> Logger;

        public async Task<User> Buscar(string userName, string password)
        {
            var user = await UserManager.FindByNameAsync(userName);
            if (user != null)
            {
                if (await UserManager.CheckPasswordAsync(user, password))
                {
                    Logger.LogInformation($"Usuario {userName} ha iniciado sesión.");
                    await SignInManager.SignInAsync(user, isPersistent: false);
                    return user;
                }
            }

            return null;
        }

        public async Task<string> GenerarToken(LoginInputModel input)
        {
            User user = await Buscar(input.Email, input.Password);
            if (user == null)
            {
                return null;
            }

            var token = TokenService.Authenticate(user);

            Context.HttpContext.Session.SetString("Token", token);
            var claims =
                new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email)
                };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await Context.HttpContext.SignInAsync
                (
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal,
                    new AuthenticationProperties { IsPersistent = true }
                );

            return token;
        }

        public bool ValidarToken(string token)
        {
            return TokenService.Validate(token);
        }

        public async Task<ResponseViewModel> RegistrarUsuario(UserInputModel input)
        {
            var user =
                new User
                {
                    FullName = input.FullName,
                    Email = input.Email,
                    UserName = input.Email
                };

            var result = await UserManager.CreateAsync(user, input.Password);
            if (result.Succeeded)
            {
                Logger.LogInformation("Se ha creado un nuevo usuario con contraseña.");
                await SignInManager.SignInAsync(user, isPersistent: false);
                return new ResponseViewModel { Success = true, Message = string.Empty };
            }

            return
                new ResponseViewModel
                {
                    Success = false,
                    Message = string.Join(" ", result.Errors.Select(s => s.Description))
                };
        }

        public async Task Cerrar()
        {
            await Context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await SignInManager.SignOutAsync();
            Context.HttpContext.Session.Clear();
        }
    }
}
