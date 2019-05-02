using EducationPortal.Data;
using EducationPortal.Repository;
using EducationPortal.Repository.Interfaces;
using EducationPortal.Repository.Repository;
using EducationPortal.Services;
using EducationPortal.Services.Interfaces;
using System;
using System.Linq;

namespace EducationPortal.Start
{
    public class UserInterface
    {
        private readonly IRegistrationService registrationService;
        private readonly IAuthorisationService authorizationService;

        private readonly IMaterialService materialService;
        private readonly ICourseService courseService;
        private readonly ISkillService skillService;

        private readonly ICourseInputService courseInputService;
        private readonly IMaterialInputService materialInputService;
        private readonly ISkillInputService skillInputService;
        private readonly IUserInfoInputService userInfoInputService;

        private readonly EducationPortalContext context;

        private readonly Session session;

        public UserInterface(IRegistrationService registrationService, 
            IAuthorisationService authorizationService,

            IMaterialInputService materialInputService,
            ICourseInputService courseInputService,
            ISkillInputService skillInputService,
            IUserInfoInputService userInfoInputService,

            IMaterialService materialService,
            ICourseService courseService,
            ISkillService skillService,

            EducationPortalContext context,
            Session session)
        {
            this.registrationService = registrationService;
            this.authorizationService = authorizationService;

            this.materialService = materialService;
            this.courseService = courseService;
            this.skillService = skillService;

            this.materialInputService = materialInputService;
            this.courseInputService = courseInputService;
            this.skillInputService = skillInputService;
            this.userInfoInputService = userInfoInputService;

            this.context = context;
            this.session = session;
        }

        private void MenuInfo()
        {
            Console.WriteLine("1.Register");
            Console.WriteLine("2.Log in");
            Console.WriteLine("3.Log out");
            Console.WriteLine("4.Authorized or not");
            Console.WriteLine("5.Show all users");
            Console.WriteLine("6.Delete user (by email)");
            Console.WriteLine("7.Clear console");
            Console.WriteLine("8.Add material");
            Console.WriteLine("9.Show all materials");
            Console.WriteLine("10.Add course");
            Console.WriteLine("11.Show all courses");
            Console.WriteLine("12.Edit course");
            Console.WriteLine("13.Add skill");
            Console.WriteLine("14.User information");
            Console.WriteLine("15.Start course");
            Console.WriteLine("16.Show started courses");
            Console.WriteLine("17.Complete course");
            Console.WriteLine("18.Show completed courses");
            Console.WriteLine("0.Exit");
        }
        internal void Functionality()
        {
            IRepository<User> userRepository = new UserRepository(context);
            var interactionCommands = new InteractionCommands();


            this.MenuInfo();
            while (true)
            {
                Console.WriteLine("Select a number and press enter!");
                var command = Console.ReadLine();
                if (command == "0")
                {
                    break;
                }

                switch (command)
                {
                    case "1":
                        registrationService.Register();
                        break;
                    case "2":
                        if(!session.IsAuthorised)
                        {
                            authorizationService.SignIn(userRepository);
                        }
                        else
                        {
                            Console.WriteLine("Already authorised");
                        }
                        break;
                    case "3":
                        if (session.IsAuthorised)
                        {
                            authorizationService.SignOut();
                        }
                        else
                        {
                            Console.WriteLine("Already unauthorised");
                        }
                        break;
                    case "4":
                        Console.WriteLine(session.IsAuthorised ? "Yes" : "No");
                        break;
                    case "5":
                        interactionCommands.Show(userRepository);
                        break;
                    case "6":
                        Console.WriteLine("Enter the email you want to delete.");
                        interactionCommands.Delete(userRepository);
                        break;
                    case "7":
                        Console.Clear();
                        MenuInfo();
                        break;
                    case "8":
                        materialInputService.MaterialInfoInput();
                        break;
                    case "9":
                        materialService.ShowMaterials();
                        break;
                    case "10":
                        courseInputService.CourseinfoInput();
                        break;
                    //case "11":
                    //    var courses = courseService.GetCourses();
                    //    if (!courses.Any())
                    //    {
                    //        Console.WriteLine("no courses found");
                    //    }
                    //    else
                    //    {
                    //        foreach (var course in courses)
                    //        {
                    //            Console.WriteLine(course);
                    //        }
                    //    }
                    //    break;
                    case "12":
                        courseInputService.CourseInfoUpdate();
                        break;
                    case "13":
                        skillInputService.SkillInfoInput();
                        break;
                    case "14":
                        userInfoInputService.PrintSessionUser();
                        break;
                    case "15":
                        courseInputService.StartCourse();
                        break;
                    case "16":
                        if(session.IsAuthorised)
                        {
                            courseService.GetStartedCourses(session.User.Email).ForEach(c => Console.WriteLine(c));
                        }
                        else
                        {
                            Console.WriteLine("You need to authorise");
                        }
                        break;
                    case "17":
                        if(session.IsAuthorised)
                        {
                            courseInputService.CompleteCourse();
                        }
                        else
                        {
                            Console.WriteLine("You need to authorise");
                        }
                        break;
                    case "18":
                        if (session.IsAuthorised)
                        {
                            courseService.GetCompletedCourses(session.User.Email).ForEach(c => Console.WriteLine(c));
                        }
                        else
                        {
                            Console.WriteLine("You need to authorise");
                        }
                        break;
                    default:
                        Console.WriteLine("Command not found!");
                        break;
                }
            }
        }
    }
}
