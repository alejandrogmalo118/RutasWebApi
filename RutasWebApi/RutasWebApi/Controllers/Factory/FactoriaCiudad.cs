using RutasWebApi.Models;
using RutasWebApi.Models.Cliente;
using RutasWebApi.Models.Config;
using RutasWebApi.Models.ModelJSON;
using System.Threading.Tasks;

namespace RutasWebApi.Controllers.Factory
{
    public class FactoriaCiudad: FactoriaDatos
    {
        private int Id { get; }
        private string NombreCiudad { get; }

        public FactoriaCiudad()
        {

        }

        public FactoriaCiudad(int id, string nombreCiudad)
        {
            Id = id;
            NombreCiudad = nombreCiudad;
            CrearDato();
        }

        public sealed override void CrearDato()
        {
            Ciudad nuevoDato = new Ciudad(Id, NombreCiudad);
            
            if(!Listas.CiudadesDisponibles.Exists(c => c.Id.Equals(nuevoDato.Id)))
            {
                Listas.CiudadesDisponibles.Add(nuevoDato);
            }

        }

        public override async Task CrearDatos()
        {
            var listaCiudades = await AutobusesCliente.LlamarApi<CiudadJsonModel.CiudadModel>(AutobusesClienteConfig.Ciudades);

            foreach (var c in listaCiudades)
            {
                var ciudad = new FactoriaCiudad(c.Id, c.NombreCiudad);
            }
        }

    }
}