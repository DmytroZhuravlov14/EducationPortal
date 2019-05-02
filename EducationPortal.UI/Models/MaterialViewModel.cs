using EducationPortal.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EducationPortal.UI.Models
{
    public class MaterialViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"www.([A-Za-z0-9-]{1,}).([A-Za-z0-9-]{1,3})",
            ErrorMessage = "Link is not valid")]
        public string Link { get; set; }

        public ICollection<Skill> Skills { get; set; }
    }
}