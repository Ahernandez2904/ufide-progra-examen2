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

        public string wind_dir { get; set; }

        public string weather_descriptions { get; set; }

        public string humidity { get; set; }

        public string wind_speed { get; set; }

        public string temperature { get; set; }

        public string name { get; set; }

        public string country { get; set; }

        public string region { get; set; }

        public string weather_icons { get; set; }
    }
}
