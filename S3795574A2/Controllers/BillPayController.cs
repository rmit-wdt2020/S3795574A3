using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using S3795574A2.Data;
using S3795574A2.Models;
using S3795574A2.ViewModel;
using System;
using System.Threading.Tasks;

namespace S3795574A2.Controllers
{
    public class BillPayController:Controller
    {
        public readonly NwbaContext _context;
        private int CustomerID => HttpContext.Session.GetInt32(nameof(Customer.CustomerID)).Value;
        public BillPayController(NwbaContext context) => _context = context;
        public async Task<IActionResult> Index()
        {
            var customer = await _context.Customers.FindAsync(CustomerID);
            return View(customer);
        }
        [HttpPost]
        public async Task<IActionResult> BillPay(BillPayViewModel billPayViewModel)
        {
            var account = await _context.Accounts.FindAsync(billPayViewModel.Account);
            var payee = await _context.Payees.FindAsync(billPayViewModel.Payee);
            if (payee == null)
                ModelState.AddModelError(nameof(billPayViewModel.Payee), "The payee does not exist.");
            if (account == null)
                ModelState.AddModelError(nameof(billPayViewModel.Account), "The account does not exist.");
            if (billPayViewModel.Amount <= 0)
                ModelState.AddModelError(nameof(billPayViewModel.Amount), "Amount must be positive.");
            if (!Utilities.CheckTwoDecimalPlaces(billPayViewModel.Amount))
                ModelState.AddModelError(nameof(billPayViewModel.Amount), "Amount cannot have more than 2 decimal places.");
            if (!billPayViewModel.Period.Equals("S") &&
                billPayViewModel.ScheduledDate.ToUniversalTime() < DateTime.UtcNow)
                ModelState.AddModelError(nameof(billPayViewModel.ScheduledDate), "Cannot set a time before now.");
            if (!ModelState.IsValid)
            {
                ViewBag.Amount = billPayViewModel.Amount;
                return View(billPayViewModel);
            }
            await billPayViewModel.CreateBillPay(_context);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> BillPay(int id)
        {
            var acc = await _context.Accounts.FindAsync(id);
            return View(
                new BillPayViewModel()
                {
                    Account = acc.AccountNumber
                });
        }
        public async Task<IActionResult> BillPayList(int id)
        {
            var acc = await _context.Accounts.FindAsync(id);
            return View(acc);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var billpay = await _context.BillPays.FindAsync(id);
            _context.BillPays.Remove(billpay);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var billpay = await _context.BillPays.FindAsync(id);
            return View(billpay);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, BillPay billPay)
        {
            var account = await _context.Accounts.FindAsync(billPay.AccountNumber);
            var payee = await _context.Payees.FindAsync(billPay.PayeeID);
            if (payee == null)
                ModelState.AddModelError(nameof(billPay.PayeeID), "The payee does not exist.");
            if (account == null)
                ModelState.AddModelError(nameof(billPay.Account), "The account does not exist.");
            if (billPay.Amount <= 0)
                ModelState.AddModelError(nameof(billPay.Amount), "Amount must be positive.");
            if (!Utilities.CheckTwoDecimalPlaces(billPay.Amount))
                ModelState.AddModelError(nameof(billPay.Amount), "Amount cannot have more than 2 decimal places.");
            if (!billPay.Period.Equals("S") &&
                billPay.ScheduleDate.ToUniversalTime() < DateTime.UtcNow)
                ModelState.AddModelError(nameof(billPay.ScheduleDate), "Cannot set a time before now.");
            if (!ModelState.IsValid)
            {
                ViewBag.Amount = billPay.Amount;
                return View(billPay);
            }
            var billpay = await _context.BillPays.FindAsync(id);
            billpay.AccountNumber = billPay.AccountNumber;
            billpay.PayeeID = billPay.PayeeID;
            billpay.Amount = billPay.Amount;
            billpay.ModifyDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
