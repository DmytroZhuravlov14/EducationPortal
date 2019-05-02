using EducationPortal.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Services.Interfaces
{
    public interface ISkillService
    {
        void Add(string name, int points);
        List<Skill> Get();
        Skill Get(string name);
        void ShowSkills();
    }
}
