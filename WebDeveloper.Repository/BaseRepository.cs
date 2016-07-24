using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace WebDeveloper.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        public int Add(T entity)
        {
            using (var db = new WebContextDb())
            {
                db.Entry(entity).State = EntityState.Added;
                return db.SaveChanges();
            }
                
        }

        public int Delete(T entity)
        {
            using (var db = new WebContextDb())
            {
                db.Entry(entity).State = EntityState.Deleted;
                return db.SaveChanges();
            }
        }

        public List<T> GetList()
        {
            using (var db = new WebContextDb())
            return db.Set<T>().ToList();

            
        }

        public int Update(T entity)
        {
            using (var db = new WebContextDb())
            {
                db.Entry(entity).State = EntityState.Modified;
                return db.SaveChanges();
            }
        }
    }
}
