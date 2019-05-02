using EducationPortal.Data;
using EducationPortal.Repository.Interfaces;
using EducationPortal.Services.Interfaces;
using EducationPortal.Start;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Services
{
    public class CourseService : ICourseService
    {
        private readonly IRepository<Course> repository;
        private readonly IMaterialService materialService;
        private readonly Session session;
        private readonly IUserService userService;

        public CourseService(IRepository<Course> repository, 
            Session session, 
            IUserService userService,
            IMaterialService materialService)
        {
            this.repository = repository;
            this.session = session;
            this.userService = userService;
            this.materialService = materialService;
        }

        //public void AddMaterialToCourse(string courseName, Material material)
        //{
        //    repository.Get().FirstOrDefault(c => c.Name == courseName).Materials.Add(material);
        //    repository.Save();
        //}

        public void CreateCourse(string name, string description, string ownerName, List<Material> materials)
        {
            var dbCourse = Get().FirstOrDefault(c => c.Name == name);
            if (dbCourse == null)
            {
                var course = new Course
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = name,
                    Description = description,
                    Owner = userService.Get(ownerName)
                };
                if (materials != null)
                {
                    materials.ForEach(m => course.Materials.Add(m));
                    foreach (var material in materials)
                    {
                        foreach (var skill in material.Skills)
                        {
                            course.Skills.Add(skill);
                        }
                    }
                }
                repository.Add(course);
                repository.Save();
            }
            else
            {
                throw new ArgumentException("Course already exists");
            }
        }

        public void StartCourse(string courseName, string userEmail)
        {
            var course = Get().FirstOrDefault(c => c.Name == courseName);
            if (course == null)
            {
                throw new NullReferenceException("course not found");
            }
            var user = userService.Get(userEmail);
            if (!user.UserCourse.Where(uc => uc.Course == course).Any())
            {
                user.UserCourse.Add(new Start.UserCourse
                {
                    UserId = user.Id,
                    User = user,
                    CourseId = course.Id,
                    Course = course
                });
            }
            else
            {
                throw new ArgumentException("already subscribed");
            }
            repository.Save();
        }

        public void ProcessCourse(string courseName, string userEmail, IEnumerable<string> names)
        {
            var course = Get().FirstOrDefault(c => c.Name == courseName);
            if (course == null)
            {
                throw new NullReferenceException("course not found");
            }
            var user = userService.Get(userEmail);
            foreach (var item in names)
            {
                user.CompletedMaterials.Add(materialService.Get(item));
            }
            repository.Save();
        }

        public List<Course> GetStartedCourses(string email)
        {
            var user = userService.Get(email);
            var userCourse = user.UserCourse.Where(uc => uc.IsCompleted == false);
            List<Course> courseList = new List<Course>();
            foreach (var uc in userCourse)
            {
                courseList.Add(uc.Course);
            }
            return courseList;
        }

        public void CompleteCourse(string courseName, string userEmail)
        {
            var course = Get().FirstOrDefault(c => c.Name == courseName);
            if (course == null)
            {
                throw new NullReferenceException("course not found");
            }
            var user = userService.Get(userEmail);
            if (!course.Materials.All(m => user.CompletedMaterials.Contains(m)))
            {
                throw new Exception("All materials is needed to be completed");
            }
            foreach (var skill in course.Skills)
            {
                var userSkills = user.UserSkill.Where(us => us.Skill == skill);
                if (!userSkills.Any())
                {
                    user.UserSkill.Add(new UserSkill
                    {
                        UserId = user.Id,
                        User = user,
                        SkillId = skill.Id,
                        Skill = skill,
                        Points = skill.Points
                    });
                }
                else
                {
                    userSkills.FirstOrDefault().Points++;
                }
            }
            user.UserCourse.FirstOrDefault(uc => uc.CourseId == course.Id).IsCompleted = true;
            repository.Save();
        }

        public List<Course> GetCompletedCourses(string email)
        {
            var user = userService.Get(email);
            var userCourse = user.UserCourse.Where(uc => uc.IsCompleted);
            List<Course> courseList = new List<Course>();
            foreach (var uc in userCourse)
            {
                courseList.Add(uc.Course);
            }
            return courseList;
        }

        public void Update(string courseName, string newName, string description, IEnumerable<string> names)
        {
            var course = repository.Get(x => x.Name == courseName, x => x).FirstOrDefault();
            if (course == null)
            {
                throw new NullReferenceException("course not found");
            }
            if (newName != null)
            {
                course.Name = newName;
            }
            if (description != null)
            {
                course.Description = description;
            }
            course.Materials.Clear();
            foreach (var item in names)
            {
                course.Materials.Add(materialService.Get(item));
            }
            repository.Save();
        }

        public IEnumerable<Course> Get()
        {
            var courses = repository.Get(x => true, x => x);
            if (!courses.Any())
            {
                return new List<Course>();
            }
            return courses;
        }
    }
}
