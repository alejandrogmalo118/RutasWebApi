using RutasWebApi.Models;
using System.Web.Mvc;

namespace RutasWebApi.Controllers
{
    public class CiudadController : BaseController
    {

        /// <summary>
        /// Vista con el listado de todas las ciudades.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View(Listas.CiudadesDisponibles);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                RepositorioC.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}