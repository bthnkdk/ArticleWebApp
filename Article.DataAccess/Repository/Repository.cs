using Article.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ArticleContext _context;
        private readonly DbSet<T> _dbSet;
        public Repository(ArticleContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public void Delete(T obj)
        {
            if (_context.Entry(obj).State==EntityState.Detached)
                        _dbSet.Attach(obj);
            _dbSet.Remove(obj);
        }

        public T Find(int id)
        {
            return _dbSet.Find(id);
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public void Insert(T obj)
        {
            _dbSet.Add(obj);
        }

        public void Update(T obj)
        {
            _dbSet.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
           
            
        }
    }
}
