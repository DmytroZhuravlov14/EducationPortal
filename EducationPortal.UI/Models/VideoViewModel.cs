using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EducationPortal.UI.Models
{
    public class VideoViewModel : MaterialViewModel
    {
        [Required]
        public string Length { get; set; }

        [Required]
        public string Quality { get; set; }
    }
}