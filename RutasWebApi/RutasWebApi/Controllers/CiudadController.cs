using System.Threading.Tasks;
using System.Web.Mvc;
using RutasWebApi.Models;

namespace RutasWebApi.Controllers
{
    public class CiudadController : BaseController
    {

        public ActionResult Index()
        {
            return View(Listas.CiudadesDisponibles);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repositorioC.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}