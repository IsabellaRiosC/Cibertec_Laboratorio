using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDeveloper.Filters;
using WebDeveloper.Repository;

namespace WebDeveloper.Areas.Personnel.Controllers
{
    [Authorize]
    [ExceptionControl]
    public class PersonBaseController<T> : Controller
        where T:class
    {
        protected IRepository<T> _repository;

        /*ANTERIOR CODIGO*/
        //public PersonBaseController()
        //{
        //    _repository = new BaseRepository<T>();
        //}/*FUE CAMBIADO POR :*/

        public PersonBaseController(IRepository<T> repository)
        {
            _repository = repository;
        }

    }
}