using Estimate.Data;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Estimate.Services
{
    public interface ICrudService<T> where T : class
    {
        IEnumerable<T> GetAll();
        T? FindById(int id);

        void Validate(T entity);
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);

        void Update(T copy, int id);
    }

    public interface ICopyable<T>
    {
        T Clone(T source);
        void CopyTo(T source, T target);
    }

    public abstract class CrudService<T> 
        : ICrudService<T>, ICopyable<T> where T : class
    {
        protected readonly AppDbContext _db;
        protected readonly DbSet<T> _set;

        public CrudService(AppDbContext db)
        {
            _db = db;
            _set = db.Set<T>();
        }

        public virtual IEnumerable<T> GetAll() => _set.ToList();

        public T? FindById(int id) => _set.Find(id);

        public void Add(T entity)
        {
            Validate(entity); 
            _set.Add(entity);
            _db.SaveChanges();
        }

        public void Update(T copy, int id)
        {
            Validate(copy);
            var origin = FindById(id);
            CopyTo(copy, origin);
            _db.SaveChanges();
        }

        // считаем, что entity прошло Validate
        public void Update(T entity)
        {
            Validate(entity);
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

        public abstract T Clone(T source);

        public abstract void CopyTo(T source, T target);

        public abstract void Validate(T entity);

        protected virtual string GetDeleteErrorMessage(T entity)
            => $"Невозможно удалить объект типа {typeof(T).Name}" +
               $" — он связан с другими данными.";
    }
}
