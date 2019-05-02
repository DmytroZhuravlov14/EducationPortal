using EducationPortal.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Services.Interfaces
{
    public interface ICourseService
    {
        void CreateCourse(string name, string description, string ownerName, List<Material> materials);
        void StartCourse(string courseName, string userName);
        void ProcessCourse(string courseName, string userEmail, IEnumerable<string> names);
        List<Course> GetStartedCourses(string email);
        void CompleteCourse(string courseName, string userEmail);
        List<Course> GetCompletedCourses(string email);
        void Update(string courseName, string name, string description, IEnumerable<string> names);
        IEnumerable<Course> Get();
    }
}
