using RutasWebApi.Models;
using RutasWebApi.Models.Cliente;
using RutasWebApi.Models.Config;
using System.Linq;
using System.Threading.Tasks;
using RutasWebApi.Models.ModelJSON;

namespace RutasWebApi.Controllers.Factory
{
    public class FactoriaCiudad: FactoriaDatos
    {
        private int Id { get; set; }
        private string NombreCiudad { get; set; }

        public FactoriaCiudad()
        {

        }

        public FactoriaCiudad(int id, string nombreCiudad)
        {
            Id = id;
            NombreCiudad = nombreCiudad;
            CrearDato();
        }

        public override void CrearDato()
        {
            Ciudad nuevoDato = new Ciudad(Id, NombreCiudad);
            Listas.CiudadesDisponibles.Add(nuevoDato);
        }

        public async override Task CrearDatos()
        {
            var listaCiudades = await AutobusesCliente.LlamarAPI<CiudadJsonModel.CiudadModel>(AutobusesClienteConfig.CIUDADES);

            foreach (var c in listaCiudades)
            {
                new FactoriaCiudad(c.Id, c.NombreCiudad);
            }
        }

    }
}