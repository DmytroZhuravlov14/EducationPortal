using EducationPortal.Data;
using EducationPortal.Repository;
using EducationPortal.Repository.Interfaces;
using EducationPortal.Repository.Repository;
using EducationPortal.Repository.SQLRepository;
using EducationPortal.Services;
using EducationPortal.Services.Interfaces;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace EducationPortal.UI.App_Start
{
    public class DIConfig
    {
        public static void ConfigureDependencies(Container container)
        {
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            container.Register<IRepository<User>, BaseRepository<User>>(Lifestyle.Scoped);
            container.Register<IRepository<Material>, BaseRepository<Material>>(Lifestyle.Scoped);
            container.Register<IRepository<Course>, BaseRepository<Course>>(Lifestyle.Scoped);
            container.Register<IRepository<Skill>, BaseRepository<Skill>>(Lifestyle.Scoped);

            container.Register<IUserInfoInputService, UserInfoInputService>();
            container.Register<IMaterialInputService, MaterialInputService>();
            container.Register<IMaterialService, MaterialService>();

            container.Register<EducationPortalContext>(Lifestyle.Scoped);
            container.Register<ICourseService, CourseService>();
            container.Register<IUserService, UserService>();
            container.Register<ISkillService, SkillService>();
            container.Register<ISkillInputService, SkillInputService>();
            container.Register<ICourseInputService, CourseInputService>();
            container.Register<Session>(Lifestyle.Scoped);

        }
    }
}