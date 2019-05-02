using EducationPortal.Data;
using EducationPortal.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Repository.SQLRepository
{
    public class CourseRepository : BaseRepository<Course>
    {
        private readonly EducationPortalContext context;

        public CourseRepository(EducationPortalContext context) : base(context)
        {
            this.context = context;
        }

        public Course Get(string name)
        {
            return context.Courses.Include(c => c.Materials.Select(m => m.Skills)).FirstOrDefault(m => m.Name == name);
        }
    }
}
