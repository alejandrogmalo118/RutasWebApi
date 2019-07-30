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
    public class Utiles
    {
        public static string TransformarTiempoDoubleString(double tiempo)
        {
            var Horas = Math.Truncate(tiempo);
            var Minutos = Math.Truncate((tiempo - Horas) * 60);

            return $"{ Horas.ToString(CultureInfo.CurrentCulture).PadLeft(2, '0') }:{ Minutos.ToString(CultureInfo.CurrentCulture).PadLeft(2, '0') }:00";
        }

        public static double TransformarTiempoStringDouble(string tiempo)
        {
            return (double)Convert.ToDecimal(TimeSpan.Parse(tiempo).TotalHours);
        }

        public static string ModificarNombreCiudad(string nombre)
        {
            byte[] TempBytes = Encoding.GetEncoding("ISO-8859-8").GetBytes(nombre);
            return (Encoding.UTF8.GetString(TempBytes)).ToUpper();
        }

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


        public static async Task<List<List<Ruta>>> ListaFinalRutas(Ruta ruta, List<Ruta> listaRutas)
        {
            List<int> ciudadesExp = new List<int>();

            int IdDestino = ruta.Origen;
            int IdOrigen = ruta.Destino;

            ciudadesExp.Add(IdOrigen);

            List<Nodo> listaNodos = new List<Nodo>();

            foreach (var r in listaRutas)
            {
                Nodo n = new Nodo(r.Origen, r.Destino);
                n.CosteDecimal = r.Km;
                listaNodos.Add(n);
            }

            List<List<Nodo>> listaFinalNodos = new List<List<Nodo>>();
            listaFinalNodos = AlgoritmoTodasRutas.algoritmoRutas(listaFinalNodos, IdOrigen, IdDestino, ciudadesExp,
                new List<Nodo>(), listaNodos);

            List<List<Ruta>> listaFinalRutas = new List<List<Ruta>>();
            List<Ruta> listaInterna;
            foreach (var vru in listaFinalNodos)
            {
                listaInterna = new List<Ruta>();

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