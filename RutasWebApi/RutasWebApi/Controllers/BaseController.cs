using RutasWebApi.Models;
using RutasWebApi.Models.Repositorio;
using System.Web.Mvc;

namespace RutasWebApi.Controllers
{
    public class BaseController : Controller
    {
        protected RepositorioGenerico<Ciudad> RepositorioC;
        protected RepositorioGenerico<Ruta> RepositorioR;

        public BaseController()
        {
            RepositorioC = new RepositorioGenerico<Ciudad>();
            RepositorioR = new RepositorioGenerico<Ruta>();

        }

    }
}