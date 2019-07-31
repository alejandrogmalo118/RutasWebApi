using RutasWebApi.Controllers.Factory;
using RutasWebApi.Models;
using RutasWebApi.Models.Utils;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RutasWebApi.Controllers
{
    public class HomeController : BaseController
    {
        /// <summary>
        /// Vista inicial en la cual se cargan los listados de ciudades y rutas desde el webservice,
        /// y que posteriormente se guardan en la base de datos local.
        /// Tambien comprueba si hay conexión a internet, si la hay recoge los datos del webservice y los almacena,
        /// si no hay conexión, accede a la base de datos para obtener los listados.
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {

            FactoriaDatos factoriaCiudad = new FactoriaCiudad();
            FactoriaDatos factoriaRuta = new FactoriaRuta();

            if (Utiles.CheckConnection())
            {
                await factoriaCiudad.CrearDatos();
                await factoriaRuta.CrearDatos();

                foreach (var c in Listas.CiudadesDisponibles)
                {
                    Ciudad ciudad = (await RepositorioC.ObtenerId(c.Id));

                    if (ciudad == null)
                    {
                        RepositorioC.Insertar(c);
                    }
                }

                foreach (var r in Listas.RutasDisponibles)
                {
                    Ruta ruta = await RepositorioR.ObtenerId(r.Id);

                    if (ruta == null)
                    {
                        RepositorioR.Insertar(r);
                    }
                }

                await RepositorioC.Save();
                await RepositorioR.Save();
            }
            else
            {
                Listas.CiudadesDisponibles = (await RepositorioC.ObtenerTodos()).ToList();
                Listas.RutasDisponibles = (await RepositorioR.ObtenerTodos()).ToList();
            }


            return View();
        }

    }
}