using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EducationPortal.UI.Models
{
    public class BookViewModel : MaterialViewModel
    {
        [Required]
        public string Author { get; set; }

        [Required]
        public int PageNumber { get; set; }

        [Required]
        public string Format { get; set; }

        [Required]
        public string Year { get; set; }
    }
}