using EducationPortal.Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Data
{
    public class Course : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public User Owner { get; set; }

        public ICollection<UserCourse> UserCourse { get; set; }

        public ICollection<Material> Materials { get; set; }

        public ICollection<Skill> Skills { get; set; }

        public Course()
        {
            this.Skills = new List<Skill>();
            this.Materials = new List<Material>();
            this.UserCourse = new List<UserCourse>();
        }

        //public override string ToString()
        //{
        //    Console.WriteLine($"Name: {Name}, Description: {Description}");

        //    if (Materials.Any())
        //    {
        //        Console.Write("Materials: \n");
        //        foreach (var material in Materials)
        //        {
        //            Console.WriteLine(material);
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("0 materials connected to this course");
        //    }

        //    return string.Empty;
        //}
    }
}
