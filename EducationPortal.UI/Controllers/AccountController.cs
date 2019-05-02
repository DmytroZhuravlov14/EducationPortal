using EducationPortal.UI.Models;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EducationPortal.Services;
using EducationPortal.Services.Interfaces;
using System.Security.Claims;
using EducationPortal.Data;

namespace EducationPortal.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService userService;

        public AccountController(IUserService userService)
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

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = null;
                try
                {
                    user = userService.Get(model.Email);
                }
                catch (ArgumentException ex)
                {
                    ViewBag.message = ex.Message;
                }
                if(user == null)
                {
                    userService.Add(model.FirstName, model.LastName, model.Login, model.Email, model.Password);
                }
                else
                {
                    ViewBag.message = "already registered";    
                }
                return RedirectToAction("Login", "Account");
            }
            return View(model);
        }

        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = userService.Get(model.Email);
                    if (user == null)
                    {
                        ModelState.AddModelError("", "Пользователь не найден");
                    }
                    if (user.Password != model.Password.GetHashCode().ToString())
                    {
                        ModelState.AddModelError("", "Неверный пароль");
                    }
                    else
                    {
                        ClaimsIdentity claim = new ClaimsIdentity("ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                        claim.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimValueTypes.String));
                        claim.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email, ClaimValueTypes.String));
                        claim.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider",
                            "OWIN Provider", ClaimValueTypes.String));

                        AuthenticationManager.SignOut();
                        AuthenticationManager.SignIn(new AuthenticationProperties
                        {
                            IsPersistent = false,
                            ExpiresUtc = new DateTimeOffset(DateTime.UtcNow.AddMinutes(15))
                        }, claim);
                        return RedirectToLocal(returnUrl);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                    return View("~/Views/Course/Error.cshtml");
                }
            }
            return View(model);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}