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
    public class GadgetsClient : IGadgetsClient
    {
        public GadgetsClient(IHttpContextAccessor context, HttpClient client, IOptions<ClientConfiguration> configuration)
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

        public async Task<List<GadgetViewModel>> Listar()
        {
            ClientConfiguration action =
                Configuration.Actions.FirstOrDefault(s => s.Nombre.Equals("List-Gadgets", StringComparison.OrdinalIgnoreCase));

            if (action == null)
            {
                return null;
            }

            List<GadgetViewModel> lista = new List<GadgetViewModel>();

            try
            {
                using (var stream = await Client.GetStreamAsync(action.Endpoint))
                {
                    lista = await JsonSerializer.DeserializeAsync<List<GadgetViewModel>>(stream, JsonOptions);
                }

                return lista;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> Borrar(int id)
        {
            ClientConfiguration action =
                Configuration.Actions.FirstOrDefault(s => s.Nombre.Equals("Delete-Gadget", StringComparison.OrdinalIgnoreCase));

            if (action == null)
            {
                return false;
            }

            try
            {
                using (var message = await Client.DeleteAsync(string.Concat(action.Endpoint, "/?id=", id)))
                {
                    if (message.IsSuccessStatusCode)
                    {
                        string response = await message.Content.ReadAsStringAsync();
                        return bool.Parse(response);
                    }
                }
            }
            catch
            {
                return false;
            }

            return false;
        }
    }
}
