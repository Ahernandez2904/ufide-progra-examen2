using Gadgets.Application.Models.ViewModels;
using Gadgets.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gadgets.Application.Contracts.Logics
{
    public interface IGadgetLogic
    {
        List<GadgetViewModel> Listar();

        void Guardar(GadgetViewModel gadget);

        void Borrar(int id);

        Gadget Buscar(string nombre);
    }
}
