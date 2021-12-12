using Gadgets.Application.Contracts.Logics;
using Gadgets.Application.Models.InputModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gadgets.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : Controller
    {
        public AuthenticationController(IUserLogic userLogic)
        {
            UserLogic = userLogic;
        }

        IUserLogic UserLogic;

        [HttpPost]
        [Route("authenticate-user")]
        public async Task<IActionResult> Authenticate(LoginInputModel user)
        {
            return Ok(await UserLogic.GenerarToken(user));
        }

        [HttpPost]
        [Route("validate-token")]
        public IActionResult Validate(string token)
        {
            return Ok(UserLogic.ValidarToken(token));
        }

        [HttpPost]
        [Route("register-account")]
        public async Task<IActionResult> Register(UserInputModel user)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new { success = false, error = "Los datos son incorretos." });
            }
            else if (!user.Password.Equals(user.ConfirmPassword))
            {
                return Ok(new { success = false, error = "Password y Confirm Password no son iguales." });
            }

            return
                Ok(await UserLogic.RegistrarUsuario(user));
        }
    }
}
