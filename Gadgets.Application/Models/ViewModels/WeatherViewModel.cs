using Gadgets.Application.Models.InputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gadgets.Application.Models.ViewModels
{
    public class WeatherViewModel
    {
        public string Query { get; set; }

        public DateTime RequestTime { get; set; }

        public string Result { get; set; }
    }
}
