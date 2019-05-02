using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EducationPortal.UI.Models
{
    public class ArticleViewModel : MaterialViewModel
    {
        [Required]
        public string PublishDate { get; set; }
    }
}