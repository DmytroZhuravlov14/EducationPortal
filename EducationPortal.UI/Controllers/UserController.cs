using EducationPortal.Data;
using EducationPortal.Services.Interfaces;
using EducationPortal.UI.Models;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EducationPortal.UI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        [Authorize]
        public ActionResult UserInfo()
        {
            var user = userService.Get(AuthenticationManager.User.Identity.Name);
            var skills = new List<Skill>();
            foreach (var us in user.UserSkill)
            {
                skills.Add(new Skill {Name = us.Skill.Name, Points = us.Points });
            }
            var userViewModel = AutoMapper.Mapper.Map<UserViewModel>(user);
            userViewModel.Skills = AutoMapper.Mapper.Map<ICollection<SkillViewModel>>(skills);
            return View(userViewModel);
        }
    }
}