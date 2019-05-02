using AutoMapper;
using EducationPortal.Data;
using EducationPortal.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EducationPortal.UI.App_Start
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Course, CourseViewModel>();
            CreateMap<Material, MaterialViewModel>();
            CreateMap<Skill, SkillViewModel>();
            CreateMap<Book, BookViewModel>();
            CreateMap<Article, ArticleViewModel>();
            CreateMap<Video, VideoViewModel>();
            CreateMap<User, UserViewModel>();
        }
    }
}