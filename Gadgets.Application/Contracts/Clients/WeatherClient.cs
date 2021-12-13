using Gadgets.Application.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gadgets.Application.Contracts.Clients
{
    public interface IWeatherClient
    {
        Task<List<WeatherViewModel>> Listar();
    }
}
