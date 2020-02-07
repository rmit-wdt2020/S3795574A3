using AdminPortal.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using S3795574A2.Data;
using S3795574A2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace S3795574A2.Controllers
{
    [AuthorizeCustomer]
    public class StatementController: Controller
    {
        private const string AccountSessionKey = "_AccountSessionKey";

        private readonly NwbaContext _context;
        private int CustomerID => HttpContext.Session.GetInt32(nameof(Customer.CustomerID)).Value;
        public StatementController(NwbaContext context) => _context = context;
        //public override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    if (!HttpContext.Session.GetInt32(nameof(Customer.CustomerID)).HasValue)
        //        filterContext.Result = new RedirectResult("/Nwba/SecureLogin");

        //}
        public async Task<IActionResult> Index()
        {
            var customer = await _context.Customers.FindAsync(CustomerID);

            return View(customer);
        }
        public async Task<IActionResult> IndexToStatement(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
                return NotFound();
            var accountJson = JsonConvert.SerializeObject(account, Formatting.Indented,
                                                            new JsonSerializerSettings
                                                            {
                                                                PreserveReferencesHandling = PreserveReferencesHandling.Objects
                                                            });
            //var accountJson = JsonConvert.SerializeObject(account);
            HttpContext.Session.SetString(AccountSessionKey, accountJson);

            return RedirectToAction(nameof(Statements));
        }
        public async Task<IActionResult> Statements(int? page = 1)
        {
            var accountJson = HttpContext.Session.GetString(AccountSessionKey);
            if (accountJson == null)
                return RedirectToAction(nameof(Index));
            var account = JsonConvert.DeserializeObject<Account>(accountJson);
            ViewBag.Account = account;

            const int pageSize = 4;
            var pagedList = await _context.Transactions.Where
                (x => x.AccountNumber == account.AccountNumber || x.DestinationAccountNumber == account.AccountNumber).OrderByDescending(x=>x.ModifyDate)
                .ToPagedListAsync(page, pageSize);
            return View(pagedList);
        }
    }
}
