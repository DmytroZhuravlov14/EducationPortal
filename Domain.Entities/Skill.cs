using EducationPortal.Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Data
{
    public class Skill : BaseEntity
    {
        public string Name { get; set; }

        public int Points { get; set; }

        public ICollection<UserSkill> UserSkill { get; set; }

        public ICollection<Material> Materials { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}, Points: {Points}";
        }
    }
}
