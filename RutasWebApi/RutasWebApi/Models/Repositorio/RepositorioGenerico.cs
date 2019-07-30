using RutasWebApi.Models.Cliente;
using RutasWebApi.Models.Config;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace RutasWebApi.Models.Repositorio
{
    public class RepositorioGenerico<T> : IRepositorioGenerico<T> where T : class
    {
        private readonly DBRutasEntities Context = null;
        private readonly DbSet<T> Table = null;

        private bool Disposed = false;

        public RepositorioGenerico()
        {
            Context = new DBRutasEntities();
            Table = Context.Set<T>();
        }

        public RepositorioGenerico(DBRutasEntities _context)
        {
            Context = _context;
            Table = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> ObtenerTodos()
        {
            return await Table.ToListAsync();
        }

        public async Task<T> ObtenerId(object id)
        {
            return await Table.FindAsync(id);
        }

        public void Insertar(T obj)
        {
            Table.Add(obj);
        }

        public void Modificar(T obj)
        {
            Table.Attach(obj);
            Context.Entry(obj).State = EntityState.Modified;
        }

        public async Task Eliminar(object id)
        {
            T Existing = await Table.FindAsync(id);
            Table.Remove(Existing);
        }

        public async Task Save()
        {
            await Context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!Disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            Disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}