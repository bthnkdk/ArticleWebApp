using Article.DataAccess.Context;
using Article.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        DbContextTransaction transaction;
        private readonly ArticleContext _context;
        private bool disposed = false;

        public UnitOfWork(ArticleContext context)
        {
            Database.SetInitializer<ArticleContext>(null);
            if (context == null)
                throw new ArgumentException("Context is null");

            _context = context;
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            return new Repository<T>(_context);
        }

        public void BeginTransaction()
        {
            transaction = _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            transaction.Commit();
        }

        public virtual void Dispose(bool dispose)
        {
            if (!disposed)
            {
                if (dispose)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Rollback()
        {
            transaction.Rollback();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();

        }


    }
}
