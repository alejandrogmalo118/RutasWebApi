using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace RutasWebApi.Models
{
    [MetadataType(typeof(RutasMetadata))]
    public partial class Ruta
    {
        public double TiempoTransformado => Utils.Utiles.TransformarTiempoStringDouble(Tiempo.ToString(CultureInfo.CurrentCulture));

        public Ruta()
        {
                
        }

        public Ruta(int id, int origen, int destino, double km, double precio)
        {
            Id = id;
            Origen = origen;
            Destino = destino;
            Km = km;
            Tiempo = TiempoTransformado;
            Precio = precio;
        }
    }

    public static partial class Listas
    {
        public static List<Ruta> RutasDisponibles = new List<Ruta>();
    }

    public class RutasMetadata
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Ruta de Origen")]
        public int Origen { get; set; }

        [DisplayName("Ruta de Destino")]
        public int Destino { get; set; }

        [DisplayName("Kilómetros")]
        public int Km { get; set; }

        public double Tiempo { get; set; }

        public double Precio { get; set; }

        [DisplayName("Ciudad Origen")]
        public virtual Ciudad Ciudad { get; set; }

        [DisplayName("Ciudad Destino")]
        public virtual Ciudad Ciudad1 { get; set; }
    }
}