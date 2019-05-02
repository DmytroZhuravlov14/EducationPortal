using EducationPortal.Services.Interfaces;
using EducationPortal.Services;
using SimpleInjector;
using EducationPortal.Repository.Interfaces;
using EducationPortal.Data;
using EducationPortal.Repository.Repository;
using EducationPortal.Repository.SQLRepository;
using EducationPortal.Repository;

namespace EducationPortal.Start
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container();

            container.Register<IAuthorisationService, ConsoleAuthorisationService>();
            container.Register<IRegistrationService, ConsoleRegistrationService>();

            container.Register<IRepository<User>, UserRepository>(Lifestyle.Singleton);
            container.Register<IRepository<Material>, MaterialRepository>(Lifestyle.Singleton);
            container.Register<IRepository<Course>, CourseRepository>(Lifestyle.Singleton);
            container.Register<IRepository<Skill>, SkillRepository>(Lifestyle.Singleton);

            container.Register<IUserInfoInputService, UserInfoInputService>();
            container.Register<IMaterialInputService, MaterialInputService>();
            container.Register<IMaterialService, MaterialService>();

            container.Register<EducationPortalContext>(Lifestyle.Singleton);
            container.Register<UserInterface>();
            container.Register<ICourseService, CourseService>();
            container.Register<IUserService, UserService>();
            container.Register<ISkillService, SkillService>();
            container.Register<ISkillInputService, SkillInputService>();
            container.Register<ICourseInputService, CourseInputService>();
            container.Register<Session>(Lifestyle.Singleton);

            container.Verify();

            UserInterface ui = container.GetInstance<UserInterface>();
            ui.Functionality();
        }
    }
}
