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
    public class WeatherLogic : IWeatherLogic
    {
        public WeatherLogic(IApplicationDbContext dbContext, IWeatherContext weatherContext)
        {
            _dbContext = dbContext;
            WeatherContext = weatherContext;
        }

        readonly IApplicationDbContext _dbContext;
        readonly IWeatherContext WeatherContext;

        public List<WeatherViewModel> Listar()
        {
            return
                _dbContext.Weather.Select
                    (
                        _ =>
                            new WeatherViewModel
                            {
                                /*Id = _.Id,
                                Nombre = _.Nombre,
                                Marca = _.Marca,
                                Precio = _.Precio,
                                Tipo = _.Tipo*/
                            }
                    ).ToList();
        }

        public void Guardar(WeatherViewModel we)
        {
            /*Weather w =
                we.Id > 0
                    ? _dbContext.Weather.FirstOrDefault(s => s.Id == we.Id) 
                    : new Weather();

            w.Query = we.Query;
            w.RequestTime = we.RequestTime;
            w.Result = we.Result;

            if (we.Id < 1)
            {
                _dbContext.Weather.Add(w);
            }

            _dbContext.Guardar();*/
        }
    }
}
