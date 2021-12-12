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
    public class GadgetsController : Controller
    {
        public GadgetsController(IGadgetLogic gadgetLogic)
        {
            _gadgetLogic = gadgetLogic;
        }

        IGadgetLogic _gadgetLogic;

        [HttpGet]
        [Route("all-gadgets")]
        public IActionResult GetGadgets()
        {
            return
                Ok(_gadgetLogic.Listar());
        }

        [HttpPost]
        [Route("save-gadget")]
        public IActionResult SaveGadget(GadgetViewModel gadget)
        {
            _gadgetLogic.Guardar(gadget);

            return Ok(new { success = true, message = String.Empty });
        }

        [HttpDelete]
        [Route("delete-gadget")]
        public IActionResult DeleteGadget(int id)
        {
            try
            {
                _gadgetLogic.Borrar(id);
                return Ok(true);
            }
            catch
            {
                return Ok(false);
            }
        }

        [HttpGet]
        [Route("search-gadget")]
        public IActionResult SearchGadget(string nombre)
        {
            return Ok(_gadgetLogic.Buscar(nombre));
        }
    }
}
