using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace againcrud.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Age { get; set; }

        public string Gender { get; set; }

        [Required]
        public string image { get; set; }

        [NotMapped]
        public HttpPostedFileBase File { get; set; }
    }
}