using System.Collections.Generic;
using System.Threading.Tasks;

namespace RutasWebApi.Models.Repositorio
{
    public interface IRepositorioGenerico<T> where T : class
    {
        Task<IEnumerable<T>> ObtenerTodos();
        Task<T> ObtenerId(object id);
        void Insertar(T obj);
        void Modificar(T obj);
        Task Eliminar(object id);
        Task Save();
        void Dispose();
    }
}
