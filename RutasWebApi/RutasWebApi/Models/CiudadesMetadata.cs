using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RutasWebApi.Models
{
    [MetadataType(typeof(CiudadesMetadata))]
    public partial class Ciudad
    {
        public Ciudad(int id, string nombreCiudad)
        {
            Id = id;
            NombreCiudad = nombreCiudad;
        }

    }
    public static partial class Listas
    {
        public static List<Ciudad> CiudadesDisponibles = new List<Ciudad>();
    }
    public class CiudadesMetadata
    {
        [DisplayName("Nombre Ciudad")]
        public string NombreCiudad { get; set; }
    }
}