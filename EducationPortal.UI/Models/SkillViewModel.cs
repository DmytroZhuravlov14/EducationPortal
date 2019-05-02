using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EducationPortal.UI.Models
{
    public class SkillViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int Points { get; set; }
    }
}