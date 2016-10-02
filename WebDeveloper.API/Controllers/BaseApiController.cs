using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebDeveloper.Repository;

namespace WebDeveloper.API.Controllers
{
   // [Authorize]
    public class BaseApiController <T> : ApiController
        where T : class
    {

        protected IRepository<T> _repository;

        public BaseApiController(IRepository<T> repository)
        {
            _repository = repository;


        }
    }
}
