using EducationPortal.Services.Helpers;
using EducationPortal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Services
{
    public class SkillInputService : ISkillInputService
    {
        private readonly ISkillService skillService;
        private readonly Session session;

        public SkillInputService(Session session, ISkillService skillService)
        {
            this.session = session;
            this.skillService = skillService;
        }

        public string SkillInfoInput()
        {
            if(session.IsAuthorised)
            {
                skillService.ShowSkills();

                string name = string.Empty;
                int points = 0;

                name = InfoUnit.GetInfoUnit(name,
                    x => !string.IsNullOrEmpty(x.ToString()) && x.ToString().Length > 1,
                    "Enter skill name",
                    @"Error! Skill name is invalid!").ToString();

                points = Convert.ToInt32(InfoUnit.GetInfoUnit(points,
                    x => !string.IsNullOrEmpty(x.ToString()),
                    "Enter points",
                    @"Error! points is invalid!"));

                skillService.Add(name, points);
                return name;
            }
            else
            {
                Console.WriteLine("You need to authorize");
                return null;
            }
        }
    }
}
