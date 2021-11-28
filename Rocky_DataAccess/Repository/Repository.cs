using Microsoft.EntityFrameworkCore;
using Rocky_DataAccess.Data;
using Rocky_DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Rocky_DataAccess.Repository
{
    /// <summary>
    /// Репозиторий
    /// </summary>
    public class Repository<T> : IRepository<T> where T : class
    {
        public Repository(DBApplicationContext dB)
        {
            _db = dB;
            this.dBSet = dB.Set<T>();
        }

        /// <summary>
        /// Класс для взаимодействия с бд
        /// </summary>
        private readonly DBApplicationContext _db;

        internal DbSet<T> dBSet;

        public void Add(T entity)
        {
            dBSet.Add(entity);
        }

        public T Find(int id)
        {
            return dBSet.Find(id);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null, bool isTracking = true)
        {
            IQueryable<T> query = filter == null ? dBSet : dBSet.Where(filter);

            if (includeProperties != null)
            {
                foreach (string includeProp in includeProperties.Split(",", StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return isTracking ? query.FirstOrDefault() : query.AsNoTracking().FirstOrDefault();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null, bool isTracking = true)
        {
            IQueryable<T> query = filter == null ? dBSet : dBSet.Where(filter);

            if (includeProperties != null)
            {
                foreach (string includeProp in includeProperties.Split(",", StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            query = orderBy == null ? query : orderBy(query);

            return isTracking ? query : query.AsNoTracking();
        }

        public void Remove(T entity)
        {
            dBSet.Remove(entity);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dBSet.RemoveRange(entity);
        }
    }
}
