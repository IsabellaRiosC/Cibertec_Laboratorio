using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDeveloper.Model;
using System.Data.Entity;

namespace WebDeveloper.Repository
{
    public class AddressRepository : BaseRepository<Address>
    {
        public Address GetById(int id)
        {
            using (var db = new WebContextDb())
            {
                return db.Address.FirstOrDefault(p=> p.AddressID == id);
            }
        }

        public List<Address> GetListBySize(int size)
        {
            using (var db = new WebContextDb())
            {
                return db.Address
                    .OrderByDescending(p=>p.ModifiedDate)
                    .Take(size).ToList();
            }
        }


    }
}
