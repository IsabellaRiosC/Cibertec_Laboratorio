using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Http;

using WebDeveloper.Model;
using WebDeveloper.Repository;

namespace WebDeveloper.API.Controllers
{
    public class PersonController : ApiController
    {
        private IRepository<Person> repository;
        public  PersonController()
        {
            repository = new BaseRepository<Person>();        
        }

        [HttpGet]
        public IHttpActionResult List() {
            return Ok(repository.PaginatedList(p=>p.ModifiedDate,1,10));

        }
    }
}