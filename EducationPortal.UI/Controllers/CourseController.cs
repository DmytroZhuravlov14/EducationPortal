using EducationPortal.Data;
using EducationPortal.Services.Interfaces;
using EducationPortal.UI.Models;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;

namespace EducationPortal.UI.Controllers
{
    [HandleError]
    public class CourseController : Controller
    {
        private readonly ICourseService courseService;
        private readonly IMaterialService materialService;
        private readonly IUserService userService;

        public CourseController(ICourseService courseService, 
            IMaterialService materialService, 
            IUserService userService)
        {
            this.courseService = courseService;
            this.materialService = materialService;
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
        [HttpGet]
        public ActionResult ShowCompletedCourses()
        {
            var completedCourses = courseService.GetCompletedCourses(AuthenticationManager.User.Identity.Name);
            ViewBag.PageName = "Completed courses";
            return View("ShowCourses", Mapper.Map<IEnumerable<CourseViewModel>>(completedCourses));
        }

        [Authorize]
        [HttpGet]
        public ActionResult ShowStartedCourses()
        {
            var startedCourses = courseService.GetStartedCourses(AuthenticationManager.User.Identity.Name);
            ViewBag.PageName = "Started courses";
            return View("ShowCourses", Mapper.Map<IEnumerable<CourseViewModel>>(startedCourses));
        }

        [HttpGet]
        public ActionResult ShowCourses()
        {
            IEnumerable<Course> courses = null;
            try
            {
                courses = courseService.Get();     
            }
            catch(NullReferenceException ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
            ViewBag.PageName = "All available courses";
            return View(Mapper.Map<IEnumerable<CourseViewModel>>(courses));
        }

        [Authorize]
        [HttpGet]
        public ActionResult GetCourseToUpdate()
        {
            ViewBag.Title = "Update Course";
            ViewBag.Courses = courseService.Get().Where(c => c.Owner == userService.Get(User.Identity.Name));
            return View("GetCourse");
        }

        [Authorize]
        [HttpPost]
        public ActionResult GetCourseToUpdate(string courseName)
        {
            ViewBag.Title = "Update Course";
            var courseUpdate = courseService.Get().FirstOrDefault(c => c.Name == courseName);
            if (courseUpdate != null)
            {
                Session["courseUpdate"] = courseUpdate;
            }
            else
            {
                ViewBag.Error = "course not found";
                return View("Error");
            }
            if (courseService.Get()
                .FirstOrDefault(c => c.Name == (Session["courseUpdate"] as Course).Name)
                .Owner.Id == userService.Get(User.Identity.Name).Id)
            {
                return RedirectToAction("UpdateCourse");
            }
            else
            {
                ViewBag.Error = "you are not the creator of this course";
                return View("Error");
            }
        }

        [Authorize]
        [HttpGet]
        [NoDirectAccess]
        public ActionResult UpdateCourse()
        {
            ViewBag.Title = "Update Course";
            ViewBag.Materials = materialService.Get();
            ViewBag.CourseMaterials = courseService.Get()
                .FirstOrDefault(c => c.Name == (Session["courseUpdate"] as Course).Name)
                .Materials
                .ToList();

            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult UpdateCourse(CourseViewModel model, string[] names)
        {
            try
            {
                courseService.Update((Session["course"] as Course).Name, model.Name, model.Description, names.Where(x => x != "false"));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
            return RedirectToAction("ShowCourses");
        }

        [Authorize]
        [HttpGet]
        public ActionResult AddCourse()
        {
            ViewBag.Materials = materialService.Get();
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddCourse(CourseViewModel model, string[] names)
        {
            if(ModelState.IsValid)
            {
                List<Material> materials = new List<Material>();
                foreach (var item in names.Where(x => x != "false"))
                {
                    Material material = null;
                    try
                    {
                        material = materialService.Get(item);
                    }
                    catch(NullReferenceException ex)
                    {
                        ViewBag.Error = ex.Message;
                        return View("Error");
                    }
                    materials.Add(material);
                }

                try
                {
                    courseService.CreateCourse(model.Name, 
                        model.Description, 
                        AuthenticationManager.User.Identity.Name, 
                        materials);
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                    return View("Error");
                }
            }
            else
            {
                ViewBag.Materials = materialService.Get();
                return View(model);
            }
            return RedirectToAction("ShowCourses");
        }

        [Authorize]
        [HttpGet]
        public ActionResult GetCourseToProcess()
        {
            ViewBag.Title = "Process course";
            ViewBag.Courses = courseService.Get()
                .Where(c => c.UserCourse.Where(uc => uc.UserId == userService.Get(User.Identity.Name).Id)
                .Where(uc => !uc.IsCompleted)
                .Any());

            return View("GetCourse");
        }

        [Authorize]
        [HttpPost]
        public ActionResult GetCourseToProcess(string courseName)
        {
            try
            {
                var courseProcessed = courseService.Get().FirstOrDefault(c => c.Name == courseName);
                if (courseProcessed != null)
                {
                    Session["courseProcessed"] = courseProcessed;
                }
                else
                {
                    throw new NullReferenceException("course not found");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
            return RedirectToAction("ProcessCourse");
        }

        [Authorize]
        [HttpGet]
        [NoDirectAccess]
        public ActionResult ProcessCourse()
        {
            ViewBag.User = userService.Get(User.Identity.Name);
            ViewBag.CourseMaterials = courseService.Get()
                .FirstOrDefault(c => c.Name == (Session["courseProcessed"] as Course).Name)
                .Materials
                .ToList();
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ProcessCourse(string[] names)
        {
            try
            {
                courseService.ProcessCourse((Session["courseProcessed"] as Course).Name, AuthenticationManager.User.Identity.Name, names.Where(x => x != "false"));
            }
            catch (NullReferenceException ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
            return RedirectToAction("ShowCourses");
        }

        [Authorize]
        [HttpGet]
        public ActionResult StartCourse()
        {
            ViewBag.Title = "Start Course";
            ViewBag.Courses = courseService.Get()
                .Where(c => !c.UserCourse.Where(uc => uc.UserId == userService.Get(User.Identity.Name).Id)
                .Any());

            return View("GetCourse");
        }

        [Authorize]
        [HttpPost]
        public ActionResult StartCourse(string courseName)
        {
            try
            {
                courseService.StartCourse(courseName, AuthenticationManager.User.Identity.Name);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
            return RedirectToAction("ShowStartedCourses");
        }

        [Authorize]
        [HttpGet]
        public ActionResult CompleteCourse()
        {
            ViewBag.Title = "Complete Course";
            ViewBag.Courses = courseService.Get()
                .Where(c => c.UserCourse.Where(uc => uc.UserId == userService.Get(User.Identity.Name).Id)
                .Where(uc => !uc.IsCompleted)
                .Any());

            return View("GetCourse");
        }

        [Authorize]
        [HttpPost]
        public ActionResult CompleteCourse(string courseName)
        {
            try
            {
                courseService.CompleteCourse(courseName, User.Identity.Name);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
            return RedirectToAction("ShowCompletedCourses");
        }
    }
}