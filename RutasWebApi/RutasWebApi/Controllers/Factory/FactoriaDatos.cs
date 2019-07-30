using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RutasWebApi.Controllers.Factory
{
    public abstract class FactoriaDatos
    {
        public abstract void CrearDato();

        public abstract Task CrearDatos();
    }
}