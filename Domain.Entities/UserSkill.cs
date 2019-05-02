using EducationPortal.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Start
{
    public class UserSkill
    {
        public string UserId { get; set; }
        public string SkillId { get; set; }
        public virtual User User { get; set; }
        public virtual Skill Skill { get; set; }
        public int Points { get; set; }
    }
}
