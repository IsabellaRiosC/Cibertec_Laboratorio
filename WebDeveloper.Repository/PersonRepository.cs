using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDeveloper.Model;

namespace WebDeveloper.Repository
{
    public class PersonRepository : BaseRepository<Person>
    {
         public Person GetById(int id)
        {

            using (var db = new WebContextDb())
            {
                return db.Persons.FirstOrDefault(p => p.PersonId == id);

            }

        }
    }
}
