using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDeveloper.Model;
using WebDeveloper.Repository;

namespace WebDeveloper.Areas.Personnel.Controllers
{
    public class PersonPhoneController : PersonBaseController<PersonPhone>
    {
        // GET: Personnel/PersonPhone
        public PersonPhoneController(IRepository<PersonPhone> repository)
            :base(repository)
        {



        }

        public ActionResult Index()
        {
            return View();
        }

        //
        public ActionResult PhoneByPerson(int id)
        {
            return PartialView("_Phone",
                _repository.ListById(x => x.BusinessEntityID == id)
                );

        }


    }
}