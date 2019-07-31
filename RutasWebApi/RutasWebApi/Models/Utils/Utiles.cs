using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BibliotecaAlgoritmo.Algoritmos;

namespace RutasWebApi.Models.Utils
{
    /// <summary>
    /// Clase con métodos estaticos para diversas utilidades.
    /// </summary>
    public class Utiles
    {
        /// <summary>
        /// Transforma una variable de tipo double a su correspondiente en string con las horas y minutos.
        /// </summary>
        /// <param name="tiempo"></param>
        /// <returns></returns>
        public static string TransformarTiempoDoubleString(double tiempo)
        {
            var horas = Math.Truncate(tiempo);
            var minutos = Math.Truncate((tiempo - horas) * 60);

            return $"{ horas.ToString(CultureInfo.CurrentCulture).PadLeft(2, '0') }:{ minutos.ToString(CultureInfo.CurrentCulture).PadLeft(2, '0') }:00";
        }

        /// <summary>
        /// Transforma una variable de tipo string con las horas y minutos a su correspondiente en double.
        /// </summary>
        /// <param name="tiempo"></param>
        /// <returns></returns>
        public static double TransformarTiempoStringDouble(string tiempo)
        {
            return (double)Convert.ToDecimal(TimeSpan.Parse(tiempo).TotalHours);
        }

        /// <summary>
        /// Modifica el nombre de la ciudad en mayúsculas.
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public static string ModificarNombreCiudad(string nombre)
        {
            byte[] tempBytes = Encoding.GetEncoding("ISO-8859-8").GetBytes(nombre);
            return Encoding.UTF8.GetString(tempBytes).ToUpper();
        }

        /// <summary>
        /// Comprueba si hay conexión a internet.
        /// </summary>
        /// <returns>bool</returns>
        public static bool CheckConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://clients3.google.com/generate_204"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Método para calcular el total de ruta entre dos ciudades
        /// </summary>
        /// <param name="ruta">IdOrigen, IdDestino</param>
        /// <param name="listaRutas">Todas las rutas disponibles</param>
        /// <returns></returns>
        public static List<List<Ruta>> ListaFinalRutas(Ruta ruta, List<Ruta> listaRutas)
        {
            List<int> ciudadesExp = new List<int>();

            int idDestino = ruta.Origen;
            int idOrigen = ruta.Destino;

            ciudadesExp.Add(idOrigen);

            List<Nodo> listaNodos = new List<Nodo>();

            foreach (var r in listaRutas)
            {
                Nodo n = new Nodo(r.Origen, r.Destino) {CosteDecimal = r.Km};
                listaNodos.Add(n);
            }

            List<List<Nodo>> listaFinalNodos = new List<List<Nodo>>();
            listaFinalNodos = AlgoritmoTodasRutas.algoritmoRutas(listaFinalNodos, idOrigen, idDestino, ciudadesExp,
                new List<Nodo>(), listaNodos);

            List<List<Ruta>> listaFinalRutas = new List<List<Ruta>>();
            foreach (var vru in listaFinalNodos)
            {
                var listaInterna = new List<Ruta>();

                foreach (var vru2 in vru)
                {
                    Ruta r = listaRutas.Select(x => x).First(x => x.Origen == vru2.IdOrigen && x.Destino == vru2.IdDestino);
                    listaInterna.Add(r);
                }
                listaInterna.Reverse();
                listaFinalRutas.Add(listaInterna);
            }

            return listaFinalRutas;
        }
    }
}