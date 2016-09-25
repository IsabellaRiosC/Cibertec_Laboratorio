using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebDeveloper.Areas.Personnel.Models;
using WebDeveloper.Model;
using WebDeveloper.Repository;

namespace WebDeveloper.Areas.Personnel.Controllers
{
    public class PersonController : PersonBaseController<Person>
    {
        public PersonController(IRepository<Person> repository) : base(repository)
        {
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(int? page, int? size)
        {
            if (!page.HasValue || !size.HasValue)
            {
                page = 1;
                size = 15;
            }
            return PartialView("_List", _repository.PaginatedList((x => x.ModifiedDate), page.Value, size.Value));
        }

        public ActionResult Create()
        {
            var model = new PersonViewModel
            {
                Person = new Person(),
                PersonTypeList = PersonType("SC"),
                EmailPromotionList = EmailPromotion("0")
            };
            return PartialView("_Create", model);
        }

        [HttpPost]
        public ActionResult Create(Person person)
        {
            if (!ModelState.IsValid)
            {
                var model = new PersonViewModel
                {
                    Person = person,
                    PersonTypeList = PersonType(person.PersonType ?? "SC"),
                    EmailPromotionList = EmailPromotion(person.EmailPromotion.ToString() ?? "0")
                };
                return PartialView("_Create", model);
            }
            person.rowguid = Guid.NewGuid();
            person.ModifiedDate = DateTime.Now;
            person.BusinessEntity = new BusinessEntity
            {
                rowguid = person.rowguid,
                ModifiedDate = person.ModifiedDate
            };
            _repository.Add(person);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public ActionResult Edit(int id)
        {
            var model = new PersonViewModel
            {
                Person = _repository.GetById(x => x.BusinessEntityID == id),
            };
            if (model.Person == null) return RedirectToAction("Index");
            model.PersonTypeList = PersonType(model.Person.PersonType);
            model.EmailPromotionList = EmailPromotion(model.Person.EmailPromotion.ToString());
            return PartialView("_Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(Person person)
        {
            if (!ModelState.IsValid) return PartialView("_Edit", person);
            _repository.Update(person);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public ActionResult Delete(int id)
        {
            var person = _repository.GetById(x => x.BusinessEntityID == id);
            if (person == null) return RedirectToAction("Index");
            return PartialView("_Delete", person);
        }

        [HttpPost]
        public ActionResult Delete(Person person)
        {
            person = _repository.GetById(x => x.BusinessEntityID == person.BusinessEntityID);
            _repository.Delete(person);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var person = _repository.GetById(x => x.BusinessEntityID == id);
            if (person == null) return RedirectToAction("Index");
            return PartialView("_Details", person);
        }

        public int PageSize(int pageSize)
        {
            var totalRecords = _repository.GetList().Count;
            return totalRecords % pageSize > 0 ? (totalRecords / pageSize) + 1 : totalRecords / pageSize;
        }

        private IEnumerable<SelectListItem> PersonType(string selected)
        {
            var list = new[]
                {
                    new SelectListItem {Value="SC",Text= "Store Contact", Selected=false},
                    new SelectListItem {Value="IN",Text= "Individual (retail) customer", Selected=false },
                    new SelectListItem {Value="SP",Text= "Sales person" , Selected=false},
                    new SelectListItem {Value="EM",Text= "Employee (non-sales)", Selected=false },
                    new SelectListItem {Value="VC",Text= "Vendor contact" , Selected=false},
                    new SelectListItem {Value="GC",Text= "General contact", Selected=false }
                };
            list.FirstOrDefault(x => x.Value == selected).Selected = true;
            return list;
        }

        private IEnumerable<SelectListItem> EmailPromotion(string selected)
        {
            var list = new[]
                {
                    new SelectListItem {Value="0",Text= "No promotions.", Selected=false},
                    new SelectListItem {Value="1",Text= "Promotion Email", Selected=false },
                    new SelectListItem {Value="2",Text= "Promotion Email and Partner Email" , Selected=false}

                };
            list.FirstOrDefault(x => x.Value == selected).Selected = true;
            return list;
        }
    }
}