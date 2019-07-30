using RutasWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RutasWebApi.Models.Cliente;
using System.Threading.Tasks;
using RutasWebApi.Models.Config;
using RutasWebApi.Models.ModelJSON;

namespace RutasWebApi.Controllers.Factory
{
    public class FactoriaRuta : FactoriaDatos
    {
        private int Id { get; set; }
        private int Origen { get; set; }
        private int Destino { get; set; }
        private double Km { get; set; }
        private string Tiempo { get; set; }
        private double Precio { get; set; }

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

        public override void CrearDato()
        {
            Ruta nuevaRuta = new Ruta(Id, Origen, Destino, Km, Tiempo, Precio);

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
            var listaRutas = await AutobusesCliente.LlamarAPI<RutaJsonModel.RutaModel>(AutobusesClienteConfig.RUTAS);

            foreach (var c in listaRutas)
            {
                new FactoriaRuta(c.Id, c.Origen, c.Destino, c.Km, c.Tiempo, c.Precio);
            }
        }
    }
}