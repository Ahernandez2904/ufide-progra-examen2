using Gadgets.Application.Contracts.Contexts;
using Gadgets.Application.Contracts.Data;
using Gadgets.Application.Contracts.Logics;
using Gadgets.Application.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using Gadgets.Domain.Entities;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
                                Query = _.Query,
                                RequestTime = _.RequestTime,
                                Result = _.Result,
                                weather_descriptions = JObject.Parse(_.Result)["current"]["weather_descriptions"][0].ToString(),
                                temperature = JObject.Parse(_.Result)["current"]["temperature"].ToString(),
                                weather_icons = JObject.Parse(_.Result)["current"]["weather_icons"][0].ToString(),
                                wind_speed = JObject.Parse(_.Result)["current"]["wind_speed"].ToString(),
                                wind_dir = JObject.Parse(_.Result)["current"]["wind_dir"].ToString(),
                                humidity = JObject.Parse(_.Result)["current"]["humidity"].ToString(),
                                name = JObject.Parse(_.Result)["location"]["name"].ToString(),
                                country = JObject.Parse(_.Result)["location"]["country"].ToString(),
                                region = JObject.Parse(_.Result)["location"]["region"].ToString()
                            }
                    ).ToList();
        }

        public void Guardar(WeatherViewModel we)
        {
            Weather w =
                we.RequestTime != null && we.Query != null
                    ? _dbContext.Weather.FirstOrDefault(s => s.RequestTime == we.RequestTime && s.Query == we.Query) 
                    : new Weather();

            w.Query = we.Query;
            w.RequestTime = we.RequestTime;
            w.Result = we.Result;

            if (we.RequestTime != null && we.Query != null)
            {
                _dbContext.Weather.Add(w);
            }

            _dbContext.Guardar();
        }
    }
}
