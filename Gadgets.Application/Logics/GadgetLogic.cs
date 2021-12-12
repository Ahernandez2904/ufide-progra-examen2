using Gadgets.Application.Contracts.Contexts;
using Gadgets.Application.Contracts.Data;
using Gadgets.Application.Contracts.Logics;
using Gadgets.Application.Models.ViewModels;
using Gadgets.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gadgets.Application.Logics
{
    public class GadgetLogic : IGadgetLogic
    {
        public GadgetLogic(IApplicationDbContext dbContext, IGadgetContext gadgetContext)
        {
            _dbContext = dbContext;
            GadgetContext = gadgetContext;
        }

        readonly IApplicationDbContext _dbContext;
        readonly IGadgetContext GadgetContext;

        public List<GadgetViewModel> Listar()
        {
            return
                _dbContext.Gadgets.Select
                    (
                        _ =>
                            new GadgetViewModel
                            {
                                Id = _.Id,
                                Nombre = _.Nombre,
                                Marca = _.Marca,
                                Precio = _.Precio,
                                Tipo = _.Tipo
                            }
                    ).ToList();
        }

        public void Guardar(GadgetViewModel gadget)
        {
            Gadget g =
                gadget.Id > 0
                    ? _dbContext.Gadgets.FirstOrDefault(s => s.Id == gadget.Id)
                    : new Gadget();

            g.Nombre = gadget.Nombre;
            g.Marca = gadget.Marca;
            g.Precio = gadget.Precio;
            g.Tipo = gadget.Tipo;

            if (gadget.Id < 1)
            {
                _dbContext.Gadgets.Add(g);
            }

            _dbContext.Guardar();
        }

        public void Borrar(int id)
        {
            Gadget g = _dbContext.Gadgets.FirstOrDefault(s => s.Id == id);

            if (g == null)
            {
                throw new KeyNotFoundException();
            }

            _dbContext.Gadgets.Remove(g);
            _dbContext.Guardar();
        }

        public Gadget Buscar(string nombre)
        {
            return GadgetContext.Buscar(nombre);
        }
    }
}
