using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDeveloper.Filters;
using WebDeveloper.Model;
using WebDeveloper.Repository;

namespace WebDeveloper.Areas.Personnel.Controllers
{
    [AuditControl]
    public class PersonController : Controller
    {
        // GET: Person
        // Address / BussinessEntity / BussinessEntityAddress / BussinessEntityContact / EmailAddress / PersonPhone
        //http://antoniogonzalezm.es/google-hacking-46-ejemplos-hacker-contrasenas-usando-google-enemigo-peor/
        private PersonRepository _person = new PersonRepository();        
        public ActionResult Index()
        {
            //return View(_person.GetList());
            return View(_person.GetListBySize(15));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Person person)
        {
            if (!ModelState.IsValid) return View(person);
            person.rowguid = Guid.NewGuid();
            person.ModifiedDate = DateTime.Now;
            person.BusinessEntity = new BusinessEntity
            {
                rowguid = person.rowguid,
                ModifiedDate = person.ModifiedDate
            };

            _person.Add(person);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var person = _person.GetById(id);
            if (person == null) return RedirectToAction("Index");
            return View(person);
        }

        [HttpPost]
        public ActionResult Edit(Person person)
        {
            if (!ModelState.IsValid) return View(person);
            _person.Update(person);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var person = _person.GetById(id);
            if (person == null) return RedirectToAction("Index");
            return View(person);
        }

        [HttpPost]
        public ActionResult Delete(Person person)
        {
            person = _person.GetCompletePersonById(person.BusinessEntityID);   
            _person.Delete(person);
            return RedirectToAction("Index");
        }


        public ActionResult Details(int id)
        {
            var person = _person.GetById(id);
            if (person == null) return RedirectToAction("Index");
            return View(person);
        }
    }
}