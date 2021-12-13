using Gadgets.Application.Configurations;
using Gadgets.Application.Contracts.Clients;
using Gadgets.Application.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Gadgets.Application.Clients
{
    public class WeatherClient : IWeatherClient
    {
        public WeatherClient(IHttpContextAccessor context, HttpClient client, IOptions<ClientConfiguration> configuration)
        {
            Context = context;
            Client = client;
            Configuration = configuration.Value;

            string token = Context?.HttpContext?.Request?.Headers[HeaderNames.Authorization].ToString() ?? string.Empty;

            Client.BaseAddress = new Uri(Configuration.Endpoint);
            Client.DefaultRequestHeaders.Clear();
            if (!string.IsNullOrEmpty(token))
            {
                Client.DefaultRequestHeaders.Add(HeaderNames.Authorization, token);
            }

            JsonOptions =
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    ReferenceHandler = ReferenceHandler.Preserve
                };
        }

        readonly IHttpContextAccessor Context;
        readonly HttpClient Client;
        readonly ClientConfiguration Configuration;
        readonly JsonSerializerOptions JsonOptions;

        public async Task<List<WeatherViewModel>> Listar()
        {
            ClientConfiguration action =
                Configuration.Actions.FirstOrDefault(s => s.Nombre.Equals("List-Weather", StringComparison.OrdinalIgnoreCase));

            if (action == null)
            {
                return null;
            }

            List<WeatherViewModel> lista = new List<WeatherViewModel>();

            try
            {
                using (var stream = await Client.GetStreamAsync(action.Endpoint))
                {
                    lista = await JsonSerializer.DeserializeAsync<List<WeatherViewModel>>(stream, JsonOptions);
                }

                return lista;
            }
            catch(Exception e)
            {
                return null;
            }
        }
    }
}
