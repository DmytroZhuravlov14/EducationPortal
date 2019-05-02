using EducationPortal.Data;
using EducationPortal.Repository.Interfaces;
using EducationPortal.Services.Helpers;
using EducationPortal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Services
{
    public class CourseInputService : ICourseInputService
    {
        private readonly IMaterialService materialService;
        private readonly IMaterialInputService materialInputService;
        private readonly ICourseService courseService;
        private readonly Session session;
        private readonly IUserService userService;

        public CourseInputService(IMaterialService materialService, 
            ICourseService courseService,
            IMaterialInputService materialInputService,
            Session session,
            IUserService userService)
        {
            this.materialService = materialService;
            this.courseService = courseService;
            this.materialInputService = materialInputService;
            this.session = session;
            this.userService = userService;
        }

        public void CourseInfoUpdate()
        {
            if(session.IsAuthorised)
            {
                var courses = courseService.Get().Where(c => c.Owner.Id == session.User.Id);
                if (!courses.Any())
                {
                    Console.WriteLine("no courses found");
                }
                else
                {
                    foreach (var c in courses)
                    {
                        Console.WriteLine(c);
                    }
                }

                string name = string.Empty;
                name = InfoUnit.GetInfoUnit(
                    name,
                    x => !(string.IsNullOrEmpty(x.ToString())),
                    "Enter name of the course you want to edit",
                    "Error! Name is invalid"
                    ).ToString();

                var course = courseService.Get().FirstOrDefault(c => c.Name == name);

                if(course.Owner.Id != session.User.Id)
                {
                    Console.WriteLine("Not allowed");
                }
                else
                {
                    string field = string.Empty;
                    field = InfoUnit.GetInfoUnit(
                       field,
                       x => !(string.IsNullOrEmpty(x.ToString()) &&
                       new List<string> { "1", "2", "3" }.Contains(x.ToString())),
                       "Enter name of the field you want to edit: \n 1.Name \n 2.Description \n 3.Materials",
                       "Error! Field is invalid"
                       ).ToString();

                    if (field == "1")
                    {
                        var newName = string.Empty;
                        newName = InfoUnit.GetInfoUnit(
                            newName,
                            x => !(string.IsNullOrEmpty(x.ToString())),
                            "Enter new name",
                            "Error! Name is invalid"
                            ).ToString();

                        //courseService.Update(course.Name, newName, null);
                    }
                    if (field == "2")
                    {
                        var newDescription = string.Empty;
                        newDescription = InfoUnit.GetInfoUnit(
                            field,
                            x => !(string.IsNullOrEmpty(x.ToString())),
                            "Enter new description",
                            "Error! Description is invalid"
                            ).ToString();

                        //courseService.Update(course.Name, null, newDescription);
                    }
                    if (field == "3")
                    {
                        Console.WriteLine(course);

                        var command = string.Empty;
                        command = InfoUnit.GetInfoUnit(
                            command,
                            x => !(string.IsNullOrEmpty(x.ToString())) &&
                            new List<string> { "1", "2" }.Contains(x.ToString()),
                            "Wanna add new material(1), add existing material(2) or delete some (3)",
                            "Error! command is invalid"
                            ).ToString();
                        if (command == "1")
                        {
                            var materialName = materialInputService.MaterialInfoInput();
                            var material = materialService.Get(materialName);
                            //courseService.AddMaterialToCourse(course.Name, material);
                        }
                        if (command == "2")
                        {
                            materialService.ShowMaterials();

                            var materialName = string.Empty;
                            materialName = InfoUnit.GetInfoUnit(
                                materialName,
                                x => !(string.IsNullOrEmpty(x.ToString())),
                                "Enter material name",
                                "Error! Material name is invalid"
                                ).ToString();

                            var material = materialService.Get(materialName);
                            //courseService.AddMaterialToCourse(course.Name, material);
                        }
                        if (command == "3")
                        {
                            var materialName = string.Empty;
                            materialName = InfoUnit.GetInfoUnit(
                                materialName,
                                x => !(string.IsNullOrEmpty(x.ToString())),
                                "Enter material name",
                                "Error! Material name is invalid"
                                ).ToString();

                            var material = materialService.Get(materialName);
                            //materialService.Delete(material.Id);
                        }
                    }
                    else
                    {
                        Console.WriteLine("You need to authorize");
                    }
                }
            }
        }

        public void StartCourse()
        {
            if(session.IsAuthorised)
            {
                var courses = courseService.Get().Where(c => !c.UserCourse.Where(uc => uc.Course.Id == c.Id).Any());
                if (!courses.Any())
                {
                    Console.WriteLine("no courses found");
                }
                else
                {
                    foreach (var c in courses)
                    {
                        Console.WriteLine(c);
                    }
                }

                string courseName = string.Empty;
                courseName = InfoUnit.GetInfoUnit(
                    courseName,
                    x => !(string.IsNullOrEmpty(x.ToString())),
                    "Enter course name",
                    "Error! Course name is invalid"
                    ).ToString();


                var course = courseService.Get().FirstOrDefault(c => c.Name == courseName).UserCourse.
                    FirstOrDefault(uc => uc.CourseId == courseService.Get().FirstOrDefault(c => c.Name == courseName).Id);
                if (course != null && course.IsCompleted == false)
                {
                    Console.WriteLine("already started");
                }
                else
                {
                    //courseService.StartCourse(courseName);
                }
            }
            else
            {
                Console.WriteLine("You need to authorize");
            }
        }

        public void CompleteCourse()
        {
            if(session.IsAuthorised)
            {
                var courses = userService.Get(session.User.Email).UserCourse.Where(uc => uc.IsCompleted == false);
                if(courses.Any())
                {
                    foreach (var item in courses)
                    {
                        Console.WriteLine(item.Course);
                    }
                    string courseName = string.Empty;
                    courseName = InfoUnit.GetInfoUnit(
                        courseName,
                        x => !(string.IsNullOrEmpty(x.ToString())),
                        "Enter course name",
                        "Error! Course name is invalid"
                        ).ToString();

                    var course = courses.FirstOrDefault(c => c.Course.Name == courseName).Course;
                    if (course != null)
                    {
                        if (course.UserCourse.FirstOrDefault(uc => uc.CourseId == course.Id).IsCompleted)
                        {
                            Console.WriteLine("already completed");
                        }
                        else
                        {
                            //courseService.CompleteCourse(courseName);
                        }
                    }
                    else
                    {
                        Console.WriteLine("course not found");
                    }
                }
                else
                {
                    Console.WriteLine("No courses to complete");
                }
            }
        }

        public void CourseinfoInput()
        {
            if (session.IsAuthorised)
            {
                var courses = courseService.Get();
                if (!courses.Any())
                {
                    Console.WriteLine("no courses found");
                }
                else
                {
                    foreach (var course in courses)
                    {
                        Console.WriteLine(course);
                    }
                }

                string name = string.Empty;
                string description = string.Empty;

                name = InfoUnit.GetInfoUnit(
                    name,
                    x => !(string.IsNullOrEmpty(x.ToString())),
                    "Enter course name",
                    "Error! Name is invalid"
                    ).ToString();

                description = InfoUnit.GetInfoUnit(
                    description,
                    x => !(string.IsNullOrEmpty(x.ToString())),
                    "Enter course description",
                    "Error! discription is invalid"
                    ).ToString();

                Console.WriteLine("Wanna add some materials?\n 1.Yes\n 2.No");
                while (true)
                {
                    var command = Console.ReadLine();
                    if (command == "1" || command == "Yes")
                    {
                        while(true)
                        {
                            Console.WriteLine("Add new(1) or existing(2)? PRESS 0 TO EXIT MATERIAL ADDING MODE");
                            var materials = new List<Material>();
                            var innerCommand = Console.ReadLine();
                            if (innerCommand == "0")
                            {
                                break;
                            }
                            if (innerCommand == "1")
                            {
                                var materialName = materialInputService.MaterialInfoInput();
                                var material = materialService.Get(materialName);
                                materials.Add(material);
                            }
                            if(innerCommand == "2")
                            {
                                materialService.ShowMaterials();
                                string materialName = string.Empty;

                                materialName = InfoUnit.GetInfoUnit(
                                    materialName,
                                    x => !(string.IsNullOrEmpty(x.ToString())),
                                    "Enter material name",
                                    "Error! Invalid material name"
                                    ).ToString();

                                var material = materialService.Get(materialName);
                                materials.Add(material);
                            }
                            //courseService.CreateCourse(name, description, materials);
                        }
                        break;
                    }
                    else
                    {
                        //courseService.CreateCourse(name, description, null);
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("You need to authorize");
            }
        }
    }
}
