
using CLassifiedsUIPortal.Models;
using CLassifiedsUIPortal.Provider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ClassifiedsUIPortal.Controllers
{
    public class AuthController : Controller
    {
        readonly IAuthProvider authprovider; //IAuthProvider is injected into the Controller class using the standrad constructor injection.

        public AuthController(IAuthProvider _authProvider)
        {

            authprovider = _authProvider;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User user)
        {


            HttpResponseMessage response1 = await authprovider.Login(user);

            if (!response1.IsSuccessStatusCode)
            {

                ViewBag.Info = "Invalid EmployeeId/password";
                return View();
            }
            else
            {

                string apiResponse1 = await response1.Content.ReadAsStringAsync();

                Jtoken jwt = JsonConvert.DeserializeObject<Jtoken>(apiResponse1);

                HttpContext.Session.SetString("token", jwt.Token);
                HttpContext.Session.SetString("user", JsonConvert.SerializeObject(user));
                HttpContext.Session.SetInt32("EmployeeId", user.EmployeeId);
                HttpContext.Session.SetString("Password", user.Password);
                ViewBag.Message = "User logged in successfully!";

                return RedirectToAction("Index", "Offer");

            }




        }

        public ActionResult Logout()
        {

            HttpContext.Session.Clear();


            return RedirectToAction("Login", "Auth");
        }

    }
}
