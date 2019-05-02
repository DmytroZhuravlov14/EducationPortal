using EducationPortal.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Start
{
    public class UserCourse : BaseEntity
    {
        public string UserId { get; set; }
        public string CourseId { get; set; }
        public virtual User User { get; set; }
        public virtual Course Course { get; set; }
        public bool IsCompleted { get; set; } = false;
    }
}
