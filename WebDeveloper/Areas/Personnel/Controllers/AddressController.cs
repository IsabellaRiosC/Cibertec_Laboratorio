using System;
using System.Web.Mvc;
using WebDeveloper.Model;
using WebDeveloper.Repository;

namespace WebDeveloper.Areas.Personnel.Controllers
{
    public class AddressController : PersonBaseController<Address>
    {
        public AddressController(IRepository<Address> repository): base(repository)
        {
        }
        public ActionResult Index()
        {
            return View(_repository.PaginatedList((x=>x.ModifiedDate),2,30));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Address address)
        {
            if (!ModelState.IsValid) return View(address);

            SetModifiedDate(address);
            address.rowguid = Guid.NewGuid();
            _repository.Add(address);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            return View(_repository.GetById(x => x.AddressID == id));
        }

        public ActionResult Details(int id)
        {
            return View(_repository.GetById(x => x.AddressID == id));
        }

        [HttpPost]
        public ActionResult Edit(Address address)
        {
            if (!ModelState.IsValid) return View(address);

            SetModifiedDate(address);
            _repository.Update(address);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            return View(_repository.GetById(x => x.AddressID == id));
        }

        [HttpPost]
        public ActionResult Delete(int? id )
        {
            if (!id.HasValue) return RedirectToAction("Index");

            var address = _repository.GetById(x => x.AddressID == id);
            if (address==null) return RedirectToAction("Index");

            _repository.Delete(address);
            return RedirectToAction("Index");
        }

        private void SetModifiedDate(Address address)
        {
            address.ModifiedDate = DateTime.Now;
        }
    }
}