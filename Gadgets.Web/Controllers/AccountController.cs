using Gadgets.Application.Contracts.Logics;
using Gadgets.Application.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gadgets.Web.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        public AccountController(IUserLogic userLogic)
        {
            UserLogic = userLogic;
        }

        readonly IUserLogic UserLogic;

        async Task ValidarToken()
        {
            var token = HttpContext.Session.GetString("Token");
            if (!string.IsNullOrEmpty(token) && !UserLogic.ValidarToken(token))
            {
                await UserLogic.Cerrar();   
            }
        }

        [HttpGet]
        public async Task<IActionResult> Register(string returnUrl = null)
        {
            await ValidarToken();
            return View(new RegisterViewModel { ReturnUrl = returnUrl ?? HttpContext.Request.Path });
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ResponseViewModel response = await UserLogic.RegistrarUsuario(model.Input);
                if (response.Success)
                {
                    return RedirectToPage(nameof(RegisterConfirmation), new { email = model.Input.Email });
                }
            }

            return View(model);
        }

        public IActionResult RegisterConfirmation(string email)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            await ValidarToken();
            return View(new LoginViewModel { ReturnUrl = returnUrl ?? Url.Content("~/") });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var token = await UserLogic.GenerarToken(model.Input);
                if (!string.IsNullOrEmpty(token))
                {
                    return LocalRedirect(model.ReturnUrl);
                }
            }

            return View(model);
        }

        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Logout(string returnUrl)
        {
            await UserLogic.Cerrar();
            return LocalRedirect(returnUrl ?? Url.Content("~/"));
        }
    }
}
