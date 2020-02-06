using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminWeb.Controllers
{
    [Route("/AdminSecureLogin")]
    public class LoginController:Controller
    {
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(string loginID, string password)
        {
            if (loginID.Equals("Admin") && password.Equals("Admin"))
            {
                HttpContext.Session.SetString("Name", "Admin");
                return RedirectToAction("Index", "Dashboard");
            }
            ModelState.AddModelError("LoginFailed", "Login failed, please try again.");
            return View();
        }

        [Route("LogoutNow")]
        public IActionResult Logout()
        {
            // Logout customer.
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }
    }
}
