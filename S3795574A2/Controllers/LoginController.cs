using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using S3795574A2.Data;
using S3795574A2.Models;
using SimpleHashing;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace S3795574A2.Controllers
{
    [Route("/Nwba/SecureLogin")]
    public class LoginController : Controller
    {
        private readonly NwbaContext _context;

        public LoginController(NwbaContext context) => _context = context;

        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(string userID, string password)
        {
            var login = await _context.Logins.FindAsync(userID);
            if (login == null || !PBKDF2.Verify(login.PasswordHash, password))
            {
                ModelState.AddModelError("LoginFailed", "Login failed, please try again.");
                return View(new Login { UserID = userID });
            }

            // Login customer.
            HttpContext.Session.SetInt32(nameof(Customer.CustomerID), login.CustomerID);
            HttpContext.Session.SetString(nameof(Customer.Name), login.Customer.Name);

            return RedirectToAction("Index", "ATM");
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
