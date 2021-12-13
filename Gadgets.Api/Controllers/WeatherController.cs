using Gadgets.Application.Contracts.Logics;
using Gadgets.Application.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gadgets.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherController : Controller
    {
        public WeatherController(IWeatherLogic wl)
        {
            logic = wl;
        }

        IWeatherLogic logic;

        [HttpGet]
        [Route("all-weather")]
        public IActionResult GetWeather()
        {
            return
                Ok(logic.Listar());
        }

        [HttpPost]
        [Route("save-weather")]
        public IActionResult SaveGadget(WeatherViewModel w)
        {
            logic.Guardar(w);

            return Ok(new { success = true, message = String.Empty });
        }
    }
}
