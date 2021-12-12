using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gadgets.Application.Models.ViewModels
{
    public class GadgetViewModel
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Marca { get; set; }

        public decimal Precio { get; set; }

        public string Tipo { get; set; }
    }
}
