using Newtonsoft.Json;
using static RutasWebApi.Models.ModelJSON.CiudadJsonModel;

namespace RutasWebApi.Models.ModelJSON
{
    public class RutaJsonModel
    {
        public class RutaModel
        {
            public string id { get; set; }

            [JsonProperty("Ciudades")]
            public CiudadModel Ciudad { get; set; }

            [JsonProperty("Ciudades1")]
            public CiudadModel Ciudad1 { get; set; }

            public int Id { get; set; }
            public int Origen { get; set; }
            public int Destino { get; set; }
            public float Km { get; set; }
            public string Tiempo { get; set; }
            public float Precio { get; set; }
        }

        public class Ciudad
        {
            public string id { get; set; }
            public int Id { get; set; }
            public string NombreCiudad { get; set; }
            public string _ref { get; set; }
        }

        public class Ciudad1
        {
            public string id { get; set; }
            public int Id { get; set; }
            public string NombreCiudad { get; set; }
            public string _ref { get; set; }
        }
    }
}