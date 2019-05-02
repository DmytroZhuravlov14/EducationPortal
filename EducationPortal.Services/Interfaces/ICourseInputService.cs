using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Services.Interfaces
{
    public interface ICourseInputService
    {
        void CourseInfoUpdate();
        void StartCourse();
        void CompleteCourse();
        void CourseinfoInput();
    }
}
