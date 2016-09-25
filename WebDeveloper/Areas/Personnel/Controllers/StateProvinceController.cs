using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDeveloper.Model;
using WebDeveloper.Repository;

namespace WebDeveloper.Areas.Personnel.Controllers
{
    public class StateProvinceController : PersonBaseController<StateProvince>
    {
        public StateProvinceController(IRepository<StateProvince> repository): base(repository)
        {
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}