using EducationPortal.Data;
using EducationPortal.Repository.Interfaces;
using EducationPortal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Services
{
    public class SkillService : ISkillService
    {
        private readonly IRepository<Skill> repository;
        private readonly Session session;

        public SkillService(IRepository<Skill> repository, Session session)
        {
            this.repository = repository;
            this.session = session;
        }

        public void Add(string name, int points)
        {
            var skill = new Skill { Name = name, Points = points };
            repository.Add(skill);
        }

        public Skill Get(string name)
        {
            return repository.Get(x => x.Name == name, x => x).FirstOrDefault();
        }

        public List<Skill> Get()
        {
            return repository.Get(x => true, x => x).ToList();
        }

        public void ShowSkills()
        {
            var skills = repository.Get(x => true, x => x);
            if (!skills.Any())
            {
                Console.WriteLine("no skills found");
            }
            else
            {
                foreach (var skill in skills)
                {
                    Console.WriteLine(skill);
                }
            }
        }
    }
}
