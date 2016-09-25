using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDeveloper.Model
{
    [Table("Person.Picture")]
    [ExcludeFromCodeCoverage]
    public class Picture
    {
        [Key]
        public int PictureId { get; set; }
        public int BusinessEntityID { get; set; }

        public string ImagePath { get; set; }
    }
}
