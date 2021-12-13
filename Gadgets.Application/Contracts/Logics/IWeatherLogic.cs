using Gadgets.Application.Models.ViewModels;
using Gadgets.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gadgets.Application.Contracts.Logics
{
    public interface IWeatherLogic
    {
        List<WeatherViewModel> Listar();

        void Guardar(WeatherViewModel weather);
    }
}
