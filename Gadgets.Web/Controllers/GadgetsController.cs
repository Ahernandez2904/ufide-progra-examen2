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
    public class GadgetsController : Controller
    {
        public GadgetsController(IGadgetsClient gadgetLogic)
        {
            GadgetsClient = gadgetLogic;
        }

        readonly IGadgetsClient GadgetsClient;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await GadgetsClient.Listar() ?? new List<GadgetViewModel>());
        }

        [HttpDelete]
        public async Task<IActionResult> Borrar(int id)
        {
            if (await GadgetsClient.Borrar(id))
            {
                return Json(new { success = true, message = "El Gadget ha sido borrado permanentemente." });
            }

            return Json(new { success = false, message = "El Gadget not pudo borrado." });
        }
    }
}
