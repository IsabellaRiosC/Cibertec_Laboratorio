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
    public class AddressController : PersonBaseController<Address>
    {
        // GET: Personnel/Address
      //    private AddressRepository _repository = new AddressRepository();
       

        public ActionResult Index()
        {
            //return View(_address.GetList());
            return View(_repository.PaginatedList((x => x.ModifiedDate),2,30));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Address address)
        {
            if (!ModelState.IsValid) return View(address);
            address.rowguid = Guid.NewGuid();
            address.ModifiedDate = DateTime.Now;
  
            _repository.Add(address);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var address = _repository.GetById(x => x.AddressID == id);
            if (address == null) return RedirectToAction("Index");
            return View(address);
        }

        [HttpPost]
        public ActionResult Edit(Address address)
        {
            if (!ModelState.IsValid) return View(address);
            address.ModifiedDate = DateTime.Now;
            _repository.Update(address);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var address = _repository.GetById(x => x.AddressID == id);
            if (address == null) return RedirectToAction("Index");
            return View(address);
        }

        [HttpPost]
        public ActionResult Delete(Address address)
        {
            _repository.Delete(address);
            return RedirectToAction("Index");
        }


        public ActionResult Details(int id)
        {
            var address = _repository.GetById(x => x.AddressID == id);
            if (address == null) return RedirectToAction("Index");
            return View(address);
        }




    }
}