using System.ComponentModel.DataAnnotations;
using System.Web;
using WebDeveloper.Model;

namespace WebDeveloper.Areas.Personnel.Models
{
    public class PictureViewModel
    {
        public Picture picture { get; set; }

        [DataType(DataType.Upload)]
        HttpPostedFileBase ImageUpload { get; set; }
    }
}