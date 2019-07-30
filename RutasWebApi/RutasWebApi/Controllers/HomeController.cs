using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using RutasWebApi.Controllers.Factory;
using RutasWebApi.Models;
using RutasWebApi.Models.Utils;

namespace RutasWebApi.Controllers
{
    public class HomeController : BaseController
    {
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
                    Ciudad ciudad = (await _repositorioC.ObtenerId(c.Id));
                    if (ciudad == null)
                    {
                        _repositorioC.Insertar(c);
                    }
                }

                foreach (var r in Listas.RutasDisponibles)
                {
                    Ruta ruta = await _repositorioR.ObtenerId(r.Id);
                    if (ruta == null)
                    {
                        _repositorioR.Insertar(r);
                    }
                }

                await _repositorioC.Save();
                await _repositorioR.Save();
            }
            else
            {
                await ObtenerDatos();
            }


            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        private async Task ObtenerDatos()
        {
            Listas.CiudadesDisponibles = (await _repositorioC.ObtenerTodos()).ToList();
            Listas.RutasDisponibles = (await _repositorioR.ObtenerTodos()).ToList();
        }

    }
}