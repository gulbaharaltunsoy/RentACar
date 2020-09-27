using EmreOrenRentACar.Code;
using EmreOrenRentACar.Dto;
using EmreOrenRentACar.Models;
using EmreOrenRentACar.ModelView;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
 
namespace EmreOrenRentACar.Controllers
{

    [AllowAnonymous]
    public class AuthController : Controller
    {
        RentalContext _context = new RentalContext();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin input)
        {
            input.Password = Helper.CreateMD5(input.Password); //Şifre : 1

            var user = _context.Users.AsQueryable().Include(x=> x.Role).FirstOrDefault(r => r.Email == input.Email && r.Password == input.Password);
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.Id.ToString(), false);

                HttpContext.Session["UserInfo"] = new UserInfo()
                {
                    Id = user.Id,
                    FullName = user.Name + " " + user.Surname,
                    RoleName = user.Role.RoleName
                };

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorMsg = "E-posta veya şifrenizi doğru giriniz.";
                return View();
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User input)
        {

            input.Password = Code.Helper.CreateMD5(input.Password);

            var user = _context.Users.AsQueryable().Include(y => y.Role).FirstOrDefault(r => r.Email == input.Email && r.Password == input.Password);
            if (user == null)
            {
                user = input;
                var userRole = _context.Roles.FirstOrDefault(x => x.RoleName == "User");
                if (userRole == null)
                {
                    userRole = _context.Roles.Add(new Role()
                    {
                        RoleName = "User"
                    });
                    _context.SaveChanges();
                    userRole = _context.Roles.FirstOrDefault(x => x.RoleName == "User");

                }
                user.RoleId = userRole.Id;

                _context.Users.Add(user);
                _context.SaveChanges();

                FormsAuthentication.SetAuthCookie(user.Id.ToString(), false);
                HttpContext.Session["UserInfo"] = new UserInfo()
                {
                    Id = user.Id,
                    FullName = user.Name + " " + user.Surname,
                    RoleName = user.Role.RoleName
                };

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorMsg = "E-posta veya şifrenizi doğru giriniz.";
                return View();
            }

        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            HttpContext.Session["UserInfo"] = null;
            return RedirectToAction("Login");
        }
    }
}