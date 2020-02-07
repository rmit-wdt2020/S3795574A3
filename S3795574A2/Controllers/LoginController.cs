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
            //User does not exist
            if (login == null)
            {
                ModelState.AddModelError("LoginFailed", "Login failed, please try again.");
                return View(new Login { UserID = userID });
            }
            //check if the user is locked by admin
            if (login.IsLocked)
            {
                ModelState.AddModelError("LockedByAdmin", "You were locked by admin. Contact us for more information.");
                return View(new Login { UserID = userID });
            }
            //check if the user is locked because too many attempts
            if (login.LockedToDate > DateTime.UtcNow)
            {
                ModelState.AddModelError("UserLocked", "Too many attempts, user is locked for 1 mins.");
                return View(new Login { UserID = userID });
            }

            if (!PBKDF2.Verify(login.PasswordHash, password))
            {
                //set the attempt to 0 for new user. Because this field is nullable.
                if (String.IsNullOrEmpty(login.Attempt.ToString()))
                    login.Attempt = 0;
                //reset attempt if the last modify date is more than 15 mins earlier than now
                if (login.ModifyDate.AddMinutes(15) < DateTime.UtcNow)
                    login.Attempt = 0;
                login.Attempt += 1;
                login.ModifyDate = DateTime.UtcNow;
                ModelState.AddModelError("Attempt", "Login Failed. Attpempt left: " + (3 - login.Attempt));
                //if user attempt 3 times in 15 mins, lock the user for 1min
                if (login.Attempt >= 3 && login.ModifyDate.AddMinutes(15) > DateTime.UtcNow)
                {                   
                    login.Attempt = 0;
                    login.LockedToDate = DateTime.UtcNow.AddMinutes(1);
                }
                
                await _context.SaveChangesAsync();
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
