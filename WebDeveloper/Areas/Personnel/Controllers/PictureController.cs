using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDeveloper.Areas.Personnel.Models;
using WebDeveloper.Model;
using WebDeveloper.Repository;

namespace WebDeveloper.Areas.Personnel.Controllers
{
    public class PictureController : PersonBaseController<Picture>
    {
        public PictureController(IRepository<Picture> repository) : base(repository)
        {
        }
        public ActionResult FileUpload(int id)
        {
            if (_repository.GetById(x => x.BusinessEntityID == id) != null)
                return RedirectToAction("Index", "Person");

            return View(new Picture { BusinessEntityID=id });
        }

        [HttpPost]
        public ActionResult FileUpload(Picture picture)
        {
            foreach (string upload in Request.Files)
            {
                if (Request.Files[upload].ContentLength == 0) continue;
                string picturePath = Server.MapPath("~/Documents/");
                if (!Directory.Exists(picturePath)) Directory.CreateDirectory(picturePath);
                string filename = Path.GetFileName(Request.Files[upload].FileName);
                Request.Files[upload].SaveAs(Path.Combine(picturePath, filename));
                picture.ImagePath = filename;
                _repository.Add(picture);
            }
            return RedirectToAction("Index", "Person");
        }

    }
}