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
    public class SkillController : Controller
    {
        private readonly ISkillService skillService;

        public SkillController(ISkillService skillService)
        {
            this.skillService = skillService;
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult AddSkill()
        {
            ViewBag.Skills = skillService.Get();
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddSkill(SkillViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    skillService.Add(model.Name, model.Points);
                    return RedirectToAction("Index", "Home");
                }
                catch (ArgumentException ex)
                {
                    ViewBag.Error = ex.Message;
                    return View("Error");
                }
            }
            else
            {
                ViewBag.Skills = skillService.Get();
                return View(model);
            }
        }
    }
}