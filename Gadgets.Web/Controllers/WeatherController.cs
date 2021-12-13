using Gadgets.Application.Contracts.Clients;
using Gadgets.Application.Contracts.Logics;
using Gadgets.Application.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gadgets.Web.Controllers
{
    [Authorize]
    public class WeatherController : Controller
    {
        public WeatherController(IWeatherClient l)
        {
            WeatherClient = l;
        }

        readonly IWeatherClient WeatherClient;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await WeatherClient.Listar() ?? new List<WeatherViewModel>());
        }
    }
}
