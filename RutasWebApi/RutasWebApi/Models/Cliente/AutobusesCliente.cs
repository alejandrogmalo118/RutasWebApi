using Newtonsoft.Json;
using RutasWebApi.Models.Config;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace RutasWebApi.Models.Cliente
{
    public static class AutobusesCliente
    {
        public static async Task<IEnumerable<T>> LlamarAPI<T>(string apiConcreta = "") where T : class
        {
            IEnumerable<T> lista = null;
            var Url = AutobusesClienteConfig.API_URL + apiConcreta;
            using (var httpClient = new HttpClient())
            {
                var Uri = new Uri(Url);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var respuesta = await httpClient.GetAsync(Uri).ConfigureAwait(false);
                if (respuesta.IsSuccessStatusCode)
                {
                    var Contenido = respuesta.Content.ReadAsStringAsync().Result;
                    lista = JsonConvert.DeserializeObject<IEnumerable<T>>(Contenido);
                }
            }
            
            return lista;
        }
    }
}