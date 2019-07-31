using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace RutasWebApi.Models.Repositorio
{
    public class RepositorioGenerico<T> : IRepositorioGenerico<T> where T : class
    {
        private readonly DBRutasEntities _context;
        private readonly DbSet<T> _table;

        private bool _disposed;

        public RepositorioGenerico()
        {
            _context = new DBRutasEntities();
            _table = _context.Set<T>();
        }

        public RepositorioGenerico(DBRutasEntities context)
        {
            this._context = context;
            _table = context.Set<T>();
        }

        public async Task<IEnumerable<T>> ObtenerTodos()
        {
            return await _table.ToListAsync();
        }

        public async Task<T> ObtenerId(object id)
        {
            return await _table.FindAsync(id);
        }

        public void Insertar(T obj)
        {
            _table.Add(obj);
        }

        public void Modificar(T obj)
        {
            _table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }

        public async Task Eliminar(object id)
        {
            T existing = await _table.FindAsync(id);
            _table.Remove(existing ?? throw new InvalidOperationException());
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}