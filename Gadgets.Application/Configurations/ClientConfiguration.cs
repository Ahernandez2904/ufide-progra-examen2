using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gadgets.Application.Configurations
{
    public class ClientConfiguration
    {
        public string Nombre { get; set; }

        public string Endpoint { get; set; }

        public List<ClientConfiguration> Actions { get; set; }
    }
}
