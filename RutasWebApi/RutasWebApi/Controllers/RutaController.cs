using RutasWebApi.Models;
using RutasWebApi.Models.Utils;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RutasWebApi.Controllers
{
    public class RutaController : BaseController
    {
        /// <summary>
        /// Vista con el listado de todas las rutas disponibles.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View(Listas.RutasDisponibles);
        }

        /// <summary>
        /// Vista con dos combos para elegir una ciudad de origen y otra de destino
        /// para calcular las rutas entre las ciudades elegidas.
        /// </summary>
        /// <returns></returns>
        public ActionResult TodasRutas()
        {
            var CiudadesOrdenadas = Listas.CiudadesDisponibles.OrderBy(c => c.NombreCiudad);

            ViewBag.Origen = new SelectList(CiudadesOrdenadas, "Id", "NombreCiudad");
            ViewBag.Destino = new SelectList(CiudadesOrdenadas, "Id", "NombreCiudad");
            
            return View();
        }

        /// <summary>
        /// Resultado de todas las rutas despues de elegir las ciudades de origen y destino.
        /// </summary>
        /// <param name="ruta">Origen, Destino</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult TodasRutasResultado([Bind(Include = "Origen, Destino")] Ruta ruta)
        {
            var listaFinalRutas = Utiles.ListaFinalRutas(ruta, Listas.RutasDisponibles);

            return View(listaFinalRutas);
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                RepositorioR.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}