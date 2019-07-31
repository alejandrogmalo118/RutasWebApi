using System.Collections.Generic;
using BibliotecaAlgoritmo.Algoritmos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RutasWebApi.Models;
using RutasWebApi.Models.Utils;

namespace RutasWebApi.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private Ciudad CrearCiudad(int id, string nombre)
        {
            var ciudad = new Ciudad {Id = id, NombreCiudad = nombre};
            return ciudad;
        }

        private Ruta CrearRuta(Ciudad ciudadO, Ciudad ciudadD, int km, double tiempo, double precio)
        {
            var ruta = new Ruta
            {
                Ciudad = ciudadO,
                Ciudad1 = ciudadD,
                Km = km,
                Tiempo = tiempo,
                Precio = precio
            };
            return ruta;
        }

        private Nodo CrearNodo(int origen, int destino, double tiempo, int km)
        {
            Nodo nodo = new Nodo(origen, destino) {CosteDecimal = tiempo, CosteEntero = km};
            return nodo;
        }


        [TestMethod]
        public void TestMethodNombreCiudad()
        {
            //Preparación
            //Acentos y mayúsculas para transformar el nombre de las ciudades
            var ciudad = CrearCiudad(2, "león");

            //Actuación
            var ciudad2Modificada = Utiles.ModificarNombreCiudad(ciudad.NombreCiudad);

            //Validación
            Assert.AreEqual(ciudad2Modificada, "LEON");
        }


        [TestMethod]
        public void TestMethodNoRepetirCiudad()
        {
            //Preparación
            //No se pueden repetir ciudades para mostrar en la lista
            Ciudad ciudad1 = CrearCiudad(1, Utiles.ModificarNombreCiudad("Zaragoza"));
            Ciudad ciudad2 = CrearCiudad(2, Utiles.ModificarNombreCiudad("zaragoza de los caballeros"));

            //Actuación

            //Validación
            Assert.AreNotEqual(ciudad1.NombreCiudad, ciudad2.NombreCiudad);
        }

        [TestMethod]
        public void TestMethodRutaOrigenDestinoDistinto()
        {
            //Preparación
            //El origen debe ser diferente al destino
            Ciudad ciudad1 = CrearCiudad(1, "ZARAGOZA");
            Ciudad ciudad2 = CrearCiudad(2, "LEON");
            Ruta ruta = CrearRuta(ciudad1, ciudad2, 0, 0, 0);

            //Actuación

            //Validación
            Assert.AreNotEqual(ruta.Ciudad.Id, ruta.Ciudad1.Id);
        }

        [TestMethod]
        public void TestMethodRutasValoresPositivos()
        {
            //Preparación
            //Obligatorios km, precio y tiempo. Que no sean 0 ni negativos
            Ciudad ciudad1 = CrearCiudad(1, "ZARAGOZA");
            Ciudad ciudad2 = CrearCiudad(2, "LEON");
            Ruta ruta = CrearRuta(ciudad1, ciudad2, 120, 9, 1.5);

            //Actuación
            var valorMinimo = 0;

            //Validación
            Assert.IsTrue(ruta.Km > valorMinimo && ruta.Precio > valorMinimo && ruta.Tiempo > valorMinimo);
        }

        [TestMethod]
        public void TestMethodTransformacionTiempoRutaStringDouble()
        {
            //Preparación
            //Transformación del tiempo correcta para guardar en el objeto
            string horaCadena = "01:30:00";
            double horaDecimal = 1.5;

            //Actuación
            double horaTransformada = Utiles.TransformarTiempoStringDouble(horaCadena);

            //Validación
            Assert.AreEqual(horaDecimal, horaTransformada);
        }

        [TestMethod]
        public void TestMethodTransformacionTiempoRutaDoubleString()
        {
            //Preparación
            //Transformación del tiempo correcta para mostrar en lista
            string horaCadena = "01:30:00";
            double horaDecimal = 1.5;

            //Actuación
            string horaTransformada = Utiles.TransformarTiempoDoubleString(horaDecimal);

            //Validación
            Assert.AreEqual(horaCadena, horaTransformada);
        }

        [TestMethod]
        public void TestMethodRutaMenorCoste()
        {
            //De origen a destino varias rutas posibles con nodos intermedios
            //Origen Madrid
            //Destino Tarragona
            //Rutas Madrid(1) - Zaragoza (2)  18€ 300km 03:00h
            //      Zaragoza - Lleida (3)  12€  150km  02:00h
            //      Lleida - Tarragona(5)  9€  90km   01:10h  (más corta excepto en tiempo)
            //
            //      Madrid - Teruel (4)    23€  350km  03:30h
            //      Teruel - Tarragona  20€  220km  02:20h

            //Preparación
            Ciudad madrid = CrearCiudad(1, "MADRID");
            Ciudad zaragoza = CrearCiudad(2, "ZARAGOZA");
            Ciudad lleida = CrearCiudad(3, "LLEIDA");
            Ciudad tarragona = CrearCiudad(5, "TARRAGONA");
            Ciudad teruel = CrearCiudad(4, "TERUEL");

            Ruta ruta1 = CrearRuta(madrid, zaragoza, 300, 3, 18);
            Ruta ruta2 = CrearRuta(zaragoza, lleida, 150, 2, 12);
            Ruta ruta3 = CrearRuta(lleida, tarragona, 90, 1.16, 9);
            List<Ruta> rutaMadridTarragona1 = new List<Ruta> {ruta1, ruta2, ruta3};

            Ruta ruta4 = CrearRuta(madrid, teruel, 350, 3.5, 23);
            Ruta ruta5 = CrearRuta(teruel, tarragona, 220, 2.33,20);
            List<Ruta> rutaMadridTarragona2 = new List<Ruta> {ruta4, ruta5};

            //Actuación
            double kmRuta1 = 0, kmRuta2 = 0;
            double precioRuta1 = 0, precioRuta2 = 0, tiempoRuta1 = 0,tiempoRuta2 = 0;
            foreach (var nodo in rutaMadridTarragona1)
            {
                precioRuta1 += nodo.Precio;
                tiempoRuta1 += nodo.Tiempo;
                kmRuta1 += nodo.Km;
            }

            foreach (var nodo in rutaMadridTarragona2)
            {
                precioRuta2 += nodo.Precio;
                tiempoRuta2 += nodo.Tiempo;
                kmRuta2 += nodo.Km;
            }

            //Validación
            Assert.IsTrue(precioRuta1 < precioRuta2 && tiempoRuta2 < tiempoRuta1 && kmRuta1 < kmRuta2);
        }

        [TestMethod]
        public void TestMethodNodosAlgoritmo()
        {
            //Preparación
            Ciudad madrid = CrearCiudad(1, "MADRID");
            Ciudad zaragoza = CrearCiudad(2, "ZARAGOZA");
            Ciudad lleida = CrearCiudad(3, "LLEIDA");
            Ciudad tarragona = CrearCiudad(5, "TARRAGONA");
            Ciudad teruel = CrearCiudad(4, "TERUEL");

            Nodo nodo1 = CrearNodo(madrid.Id, zaragoza.Id, 3, 300);
            Nodo nodo2 = CrearNodo(zaragoza.Id, lleida.Id, 2, 150);
            Nodo nodo3 = CrearNodo(lleida.Id, tarragona.Id, 1.16, 90);

            Nodo nodo4 = CrearNodo(madrid.Id, teruel.Id, 3.5, 350);
            Nodo nodo5 = CrearNodo(teruel.Id, tarragona.Id, 2.33, 220);

            List<Nodo> nodosRutas = new List<Nodo>
            {
                nodo1,
                nodo2,
                nodo3,
                nodo4,
                nodo5
            };

            List<List<Nodo>> listaFinal = new List<List<Nodo>>();
            List<int> ciudadesExpandidas = new List<int>();
            List<Nodo> rutaActual = new List<Nodo>();
            
            //Actuación
            listaFinal = AlgoritmoTodasRutas.algoritmoRutas(listaFinal, madrid.Id, tarragona.Id, ciudadesExpandidas, rutaActual, nodosRutas);

            //Validación
            Assert.IsTrue(listaFinal[0].Count == 3 && listaFinal[1].Count == 2);
        }

    }
}
