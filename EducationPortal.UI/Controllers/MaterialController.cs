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
    public class MaterialController : Controller
    {
        private readonly IMaterialService materialService;
        private readonly ISkillService skillService;

        public MaterialController(IMaterialService materialService, ISkillService skillService)
        {
            this.materialService = materialService;
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
        public ActionResult AddVideo()
        {
            ViewBag.Skills = skillService.Get();
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddVideo(VideoViewModel model, string[] names)
        {
            if (ModelState.IsValid)
            {
                List<Skill> skills = new List<Skill>();
                foreach (var item in names.Where(x => x != "false"))
                {
                    skills.Add(skillService.Get(item));
                }
                try
                {
                    materialService.CreateVideo(model.Name, 
                        model.Link, 
                        model.Length, 
                        model.Quality, 
                        skills);
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

        [Authorize]
        [HttpGet]
        public ActionResult AddArticle()
        {
            ViewBag.Skills = skillService.Get();
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddArticle(ArticleViewModel model, string[] names)
        {
            if (ModelState.IsValid)
            {
                List<Skill> skills = new List<Skill>();
                foreach (var item in names.Where(x => x != "false"))
                {
                    skills.Add(skillService.Get(item));
                }
                try
                {
                    materialService.CreateArticle(model.Name, 
                        model.Link, 
                        model.PublishDate, 
                        skills);
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

        [Authorize]
        [HttpGet]
        public ActionResult AddBook()
        {
            ViewBag.Skills = skillService.Get();
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddBook(BookViewModel model, string[] names)
        {
            if (ModelState.IsValid)
            {
                List<Skill> skills = new List<Skill>();
                foreach (var item in names.Where(x => x != "false"))
                {
                    skills.Add(skillService.Get(item));
                }
                try
                {
                    materialService.CreateBook(model.Name,
                        model.Link,
                        model.Author,
                        model.PageNumber,
                        model.Format,
                        model.Year,
                        skills);
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