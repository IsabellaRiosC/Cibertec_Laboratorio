using System.Collections.Generic;
using System.Web.Mvc;
using WebDeveloper.Model;

namespace WebDeveloper.Areas.Personnel.Models
{
    public class PersonViewModel
    {
        public Person Person { get; set; }
        public IEnumerable<SelectListItem> PersonTypeList { get; set; }
        public IEnumerable<SelectListItem> EmailPromotionList { get; set; }
    }
}