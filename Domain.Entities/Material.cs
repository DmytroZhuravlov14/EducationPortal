using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Data
{
    public abstract class Material : BaseEntity
    {
        public string Name { get; set; }

        public string Link { get; set; }

        public ICollection<Course> Courses { get; set; }

        public ICollection<Skill> Skills { get; set; }

        public Material()
        {
            this.Skills = new List<Skill>();
        }

        public override string ToString()
        {
            Console.WriteLine($"Name: {Name}, Link: {Link}");

            if (Skills.Any())
            {
                Console.Write("Skills: \n");
                foreach (var skill in Skills)
                {
                    Console.WriteLine(skill);
                }
            }
            else
            {
                Console.WriteLine("0 skills connected to this material");
            }
            return string.Empty;
        }
    }
}
