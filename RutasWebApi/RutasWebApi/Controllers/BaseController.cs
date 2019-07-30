using System.Linq;
using RutasWebApi.Models.Repositorio;
using System.Web.Mvc;
using RutasWebApi.Models;
using RutasWebApi.Controllers.Factory;
using RutasWebApi.Models.Utils;

namespace RutasWebApi.Controllers
{
    public class BaseController : Controller
    {
        protected RepositorioGenerico<Ciudad> _repositorioC;
        protected RepositorioGenerico<Ruta> _repositorioR;

        public BaseController()
        {
            _repositorioC = new RepositorioGenerico<Ciudad>();
            _repositorioR = new RepositorioGenerico<Ruta>();

        }

    }
}