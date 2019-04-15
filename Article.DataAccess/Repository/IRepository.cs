using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.DataAccess.Repository
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();

        T Find(int id);

        void Insert(T obj);

        void Update(T obj);

        void Delete(T obj);
       
    }
}
