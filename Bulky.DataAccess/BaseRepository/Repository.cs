using Bulky.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDBContext dbContext;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDBContext db)
        {
            dbContext = db;
            this.dbSet = dbContext.Set<T>();
        }
        public void Add(T entity)
        {
           var db = dbSet.Add(entity);
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> db = dbSet;
            return db.ToList();
        }

        public T GetById(Expression<Func<T, bool>> Filter)
        {
            IQueryable<T> db = dbSet;
            db = db.Where(Filter);
            return db.FirstOrDefault();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
           
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }
    }
}
