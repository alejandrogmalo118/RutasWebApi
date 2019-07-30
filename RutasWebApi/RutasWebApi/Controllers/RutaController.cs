using RutasWebApi.Models;
using RutasWebApi.Models.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace RutasWebApi.Controllers
{
    public class RutaController : BaseController
    {

        public ActionResult Index()
        {
            return View(Listas.RutasDisponibles);
        }

        public ActionResult TodasRutas()
        {
            var CiudadesOrdenadas = Listas.CiudadesDisponibles.OrderBy(c => c.NombreCiudad);

            ViewBag.Origen = new SelectList(CiudadesOrdenadas, "Id", "NombreCiudad");
            ViewBag.Destino = new SelectList(CiudadesOrdenadas, "Id", "NombreCiudad");
            
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> TodasRutasResultado([Bind(Include = "Origen, Destino")] Ruta ruta)
        {
            var listaFinalRutas = await Utiles.ListaFinalRutas(ruta, Listas.RutasDisponibles);

            return View(listaFinalRutas);
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repositorioR.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}