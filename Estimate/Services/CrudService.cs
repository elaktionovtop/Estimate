using Estimate.Data;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estimate.Services
{
    public interface ICrudService<T> where T : class
    {
        IEnumerable<T> GetAll();
        T? FindById(int id);
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
    }

    public class CrudService<T> : ICrudService<T> where T : class
    {
        private readonly AppDbContext _db;
        private readonly DbSet<T> _set;

        public CrudService(AppDbContext db)
        {
            _db = db;
            _set = db.Set<T>();
        }

        public IEnumerable<T> GetAll() => _set.ToList();

        public T? FindById(int id) => _set.Find(id);

        public void Add(T entity)
        {
            Validate(entity); 
            _set.Add(entity);
            _db.SaveChanges();
        }

        public void Update(T entity)
        {
            Validate(entity); 
            _set.Update(entity);
            _db.SaveChanges();
        }

        public virtual void Remove(T entity)
        {
            try
            {
                _set.Remove(entity);
                _db.SaveChanges();
            }
            catch(DbUpdateException ex)
            {
                throw new InvalidOperationException(GetDeleteErrorMessage(entity), ex);
            }
        }

        public virtual void Validate(T entity)
        {
            // по умолчанию ничего не делает
        }

        protected virtual string GetDeleteErrorMessage(T entity)
        {
            return $"Невозможно удалить объект типа {typeof(T).Name} — он связан с другими данными.";
        }

    }
}
