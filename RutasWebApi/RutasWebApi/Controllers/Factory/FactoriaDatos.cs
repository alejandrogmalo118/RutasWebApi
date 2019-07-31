using System.Threading.Tasks;

namespace RutasWebApi.Controllers.Factory
{
    public abstract class FactoriaDatos
    {
        public abstract void CrearDato();

        public abstract Task CrearDatos();
    }
}