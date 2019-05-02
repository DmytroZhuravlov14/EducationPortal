using EducationPortal.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Start.DTOs
{
    public class MaterialDTO
    {
        public string Name { get; set; }

        public string Link { get; set; }

        public List<Skill> Skills { get; set; }

        public MaterialDTO(string name, string link, List<Skill> skills)
        {
            this.Name = name;
            this.Link = link;
            this.Skills = skills;
        }
    }
}
