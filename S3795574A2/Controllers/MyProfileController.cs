using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using S3795574A2.Data;
using S3795574A2.Models;
using SimpleHashing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace S3795574A2.Controllers
{
    public class MyProfileController : Controller
    {
        private readonly NwbaContext _context;
        private int CustomerID => HttpContext.Session.GetInt32(nameof(Customer.CustomerID)).Value;
        public MyProfileController(NwbaContext context) => _context = context;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!HttpContext.Session.GetInt32(nameof(Customer.CustomerID)).HasValue)
                filterContext.Result = new RedirectResult("/Nwba/SecureLogin");

        }
        public async Task<IActionResult> Index()
        {
            //Redirect to login page
            //if (!HttpContext.Session.GetInt32(nameof(Customer.CustomerID)).HasValue)
            //    return Redirect("/Nwba/SecureLogin");
            var customer = await _context.Customers.FindAsync(CustomerID);

            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Index(Customer newCustomerInfo)
        {
            var customer = await _context.Customers.FindAsync(CustomerID);
            if(newCustomerInfo.Name == null)
                ModelState.AddModelError("Name", "Customer Name cannot be empty.");
            if (newCustomerInfo.Phone == null)
                ModelState.AddModelError("Phone", "Customer phone number cannot be empty.");
            if (!ModelState.IsValid)
            {
                return View(newCustomerInfo);
            }
            customer.Name = newCustomerInfo.Name;
            customer.Address = newCustomerInfo.Address;
            customer.City = newCustomerInfo.City;
            customer.States = newCustomerInfo.States;
            customer.Phone = newCustomerInfo.Phone;
            customer.PostCode = newCustomerInfo.PostCode;
            customer.TFN = newCustomerInfo.TFN;

            await _context.SaveChangesAsync();
            return View(customer);
        }
        public async Task<IActionResult> Password(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            return View(customer);
        }
        [HttpPost]
        public async Task<IActionResult> Password(string oldPassword,string newPassword,string newPasswordAgain)
        {
            //Redirect to login page
            if (!HttpContext.Session.GetInt32(nameof(Customer.CustomerID)).HasValue)
                return Redirect("/Nwba/SecureLogin");
            var userID = _context.Logins.Where(x => x.CustomerID == CustomerID).Select(x => x.UserID).FirstOrDefault();
            var login = await _context.Logins.FindAsync(userID);
            var customer = await _context.Customers.FindAsync(CustomerID);
            if (login == null || !PBKDF2.Verify(login.PasswordHash, oldPassword))
            {
                ModelState.AddModelError("oldPassword", "Password is not correct.");
                return View(customer);
            }
            if (!newPassword.Equals(newPasswordAgain))
            {
                ModelState.AddModelError("newPassword", "Password is not match.");
                return View(customer);
            }
            login.PasswordHash = PBKDF2.Hash(newPassword);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Password));
        }
    }
}
