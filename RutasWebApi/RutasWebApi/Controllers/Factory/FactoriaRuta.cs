using RutasWebApi.Models;
using RutasWebApi.Models.Cliente;
using RutasWebApi.Models.Config;
using RutasWebApi.Models.ModelJSON;
using System.Threading.Tasks;

namespace RutasWebApi.Controllers.Factory
{
    public class FactoriaRuta : FactoriaDatos
    {
        private int Id { get; }
        private int Origen { get; }
        private int Destino { get; }
        private double Km { get; }
        private string Tiempo { get; }
        private double Precio { get; }

        public FactoriaRuta()
        {

        }

        public FactoriaRuta(int id, int origen, int destino, double km, string tiempo, double precio)
        {
            Id = id;
            Origen = origen;
            Destino = destino;
            Km = km;
            Tiempo = tiempo;
            Precio = precio;
            CrearDato();
        }

        public sealed override void CrearDato()
        {
            Ruta nuevaRuta = new Ruta(Id, Origen, Destino, Km, Precio);

            if (!Listas.CiudadesDisponibles.Count.Equals(0))
            {
                foreach (var ciudad in Listas.CiudadesDisponibles)
                {
                    if (nuevaRuta.Origen.Equals(ciudad.Id))
                    {
                        nuevaRuta.Ciudad1 = ciudad;
                    }

                    if (nuevaRuta.Destino.Equals(ciudad.Id))
                    {
                        nuevaRuta.Ciudad = ciudad;
                    }
                }
            }

            if (!Listas.RutasDisponibles.Exists(c => c.Id.Equals(nuevaRuta.Id)))
            {
                Listas.RutasDisponibles.Add(nuevaRuta);
            }

        }

        public override async Task CrearDatos()
        {
            var listaRutas = await AutobusesCliente.LlamarApi<RutaJsonModel.RutaModel>(AutobusesClienteConfig.Rutas);

            foreach (var c in listaRutas)
            {
                var ruta = new FactoriaRuta(c.Id, c.Origen, c.Destino, c.Km, c.Tiempo, c.Precio);
            }
        }
    }
}