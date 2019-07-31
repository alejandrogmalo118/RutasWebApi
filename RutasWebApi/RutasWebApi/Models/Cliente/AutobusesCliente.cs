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
        public static async Task<IEnumerable<T>> LlamarApi<T>(string apiConcreta = "") where T : class
        {
            IEnumerable<T> lista = null;
            var url = AutobusesClienteConfig.ApiUrl + apiConcreta;
            using (var httpClient = new HttpClient())
            {
                var uri = new Uri(url);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var respuesta = await httpClient.GetAsync(uri).ConfigureAwait(false);
                if (respuesta.IsSuccessStatusCode)
                {
                    var contenido = respuesta.Content.ReadAsStringAsync().Result;
                    lista = JsonConvert.DeserializeObject<IEnumerable<T>>(contenido);
                }
            }
            
            return lista;
        }
    }
}