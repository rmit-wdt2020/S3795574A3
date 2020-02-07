using AdminPortal.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using S3795574A2.Data;
using S3795574A2.Models;
using S3795574A2.ViewModel;
using System.Linq;
using System.Threading.Tasks;

namespace S3795574A2.Controllers
{
    [AuthorizeCustomer]
    public class ATMController: Controller
    {
        private readonly NwbaContext _context;
        private int CustomerID => HttpContext.Session.GetInt32(nameof(Customer.CustomerID)).Value;

        public ATMController(NwbaContext context) => _context = context;
        //public override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    if (!HttpContext.Session.GetInt32(nameof(Customer.CustomerID)).HasValue)
        //        filterContext.Result = new RedirectResult("/Nwba/SecureLogin");

        //}
        public async Task<IActionResult> Index()
        {
            ////Redirect to login page
            //if(!HttpContext.Session.GetInt32(nameof(Customer.CustomerID)).HasValue)
            //    return Redirect("/Nwba/SecureLogin");
            var customer = await _context.Customers.FindAsync(CustomerID);

            return View(customer);
        }
        public async Task<IActionResult> Transaction(int id) {
            var account = await _context.Accounts.FindAsync(id);
            TransactionViewModel transactionViewModel = new TransactionViewModel
            {
                AccountNumber = account.AccountNumber,
                Balance = account.Balance,
                AccountType = account.AccountType
            };

            return View(transactionViewModel);
        } 

        [HttpPost]
        public async Task<IActionResult> Transaction(int id,TransactionViewModel transactionViewModel)
        {
            var account = await _context.Accounts.FindAsync(id);
            var destinationAccount = await _context.Accounts.FindAsync(transactionViewModel.DestinationAccountNumber);
            var countRows = _context.Transactions.Where(x => x.AccountNumber == id).Count();
            transactionViewModel.AccountNumber = id;
            
            if (transactionViewModel.SelecteID == 3)
            {
                //check if destination number is available
                if (destinationAccount == null)
                    ModelState.AddModelError("DestinationAccountNumber", "Destination Account is not available");                   
                if(!transactionViewModel.CheckBalance())
                    ModelState.AddModelError("insufficientBalance", "insufficient balance.");
            }
            if(transactionViewModel.SelecteID == 2)
            {
                if(!transactionViewModel.CheckBalance())
                    ModelState.AddModelError("insufficientBalance", "insufficient balance.");
            }
            if (transactionViewModel.Amount <= 0)
                ModelState.AddModelError(nameof(transactionViewModel.Amount), "Amount must be positive.");
            if (!Utilities.CheckTwoDecimalPlaces(transactionViewModel.Amount))
                ModelState.AddModelError(nameof(transactionViewModel.Amount), "Amount cannot have more than 2 decimal places.");
            if (!ModelState.IsValid)
            {
                ViewBag.Amount = transactionViewModel.Amount;
                return View(transactionViewModel);
            }
            //apply account operation basing on user's behavor
            switch (transactionViewModel.SelecteID)
            {
                case 1:
                    account.Deposit(transactionViewModel.Amount);
                    account.AddTransaction(TransactionType.Deposit, transactionViewModel.Amount, transactionViewModel.Comment);
                    break;
                case 2:
                    account.Withdraw(transactionViewModel.Amount);
                    account.AddTransaction(TransactionType.Withdraw, transactionViewModel.Amount, transactionViewModel.Comment);
                    //apply service fee if there are more than 4 transactions
                    if(countRows > 4)
                    {
                        account.Withdraw(TransactionViewModel.WithdrawFee);
                        account.AddTransaction(TransactionType.ServiceFee, TransactionViewModel.WithdrawFee,"Withdraw Fee");
                    }
                    break;
                case 3:
                    account.Withdraw(transactionViewModel.Amount);
                    destinationAccount.Deposit(transactionViewModel.Amount);
                    account.AddTransaction(TransactionType.Transfer, transactionViewModel.DestinationAccountNumber,transactionViewModel.Amount, transactionViewModel.Comment);
                    //apply service fee if there are more than 4 transactions
                    if (countRows > 4)
                    {
                        account.Withdraw(TransactionViewModel.TransferFee);
                        account.AddTransaction(TransactionType.ServiceFee, TransactionViewModel.TransferFee, "Transfer Fee");
                    }
                    break;
                default:
                    break;
            }
            
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
