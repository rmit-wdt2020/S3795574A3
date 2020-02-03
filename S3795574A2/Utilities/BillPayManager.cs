using S3795574A2.Data;
using S3795574A2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace S3795574A2
{
    public class BillPayManager
    {
        private readonly NwbaContext _context;
        public BillPayManager(NwbaContext context) => _context = context;
        //umcomment this to run monthly bill every 10 seconds
        //int everyTenSecond = 0;
        int quarterly = 0;
        int monthly = 0;
        int annually = 0;
        public async Task Run()
        {
            var billPays = _context.BillPays.ToList<BillPay>();
            foreach (var bill in billPays)
            {
                // Run Once and delete the billpay record
                if (bill.Period.ToString().Equals("S") && bill.ScheduleDate < DateTime.UtcNow)
                {
                    var account = _context.Accounts.Where(x => x.AccountNumber == bill.AccountNumber).FirstOrDefault();
                    account.Withdraw(bill.Amount);
                    account.AddTransaction(TransactionType.BillPay, bill.Amount,"PayeeID: " + bill.PayeeID.ToString());
                    _context.BillPays.Remove(bill);
                    await _context.SaveChangesAsync();
                }
                //monthly
                if (bill.Period.ToString().Equals("M") && bill.ScheduleDate < DateTime.UtcNow)
                {
                    var timeSpan = DateTime.UtcNow - bill.ScheduleDate;
                    var numberOfMonth = timeSpan.TotalSeconds / TimeSpan.FromDays(30).TotalSeconds;
                    if (numberOfMonth > monthly)
                    {
                        monthly += 1;
                        var account = _context.Accounts.Where(x => x.AccountNumber == bill.AccountNumber).FirstOrDefault();
                        account.Withdraw(bill.Amount);
                        account.AddTransaction(TransactionType.BillPay, bill.Amount, "PayeeID: " + bill.PayeeID.ToString());
                        await _context.SaveChangesAsync();
                    }
                }
                //quarterly
                if (bill.Period.ToString().Equals("Q") && bill.ScheduleDate < DateTime.UtcNow)
                {
                    var timeSpan = DateTime.UtcNow - bill.ScheduleDate;
                    var numberOfQuarter = timeSpan.TotalSeconds / TimeSpan.FromDays(90).TotalSeconds;
                    if (numberOfQuarter > quarterly)
                    {
                        quarterly += 1;
                        var account = _context.Accounts.Where(x => x.AccountNumber == bill.AccountNumber).FirstOrDefault();
                        account.Withdraw(bill.Amount);
                        account.AddTransaction(TransactionType.BillPay, bill.Amount, "PayeeID: " + bill.PayeeID.ToString());
                        await _context.SaveChangesAsync();
                    }
                }
                //annually
                if (bill.Period.ToString().Equals("Y") && bill.ScheduleDate < DateTime.UtcNow)
                {
                    var timeSpan = DateTime.UtcNow - bill.ScheduleDate;
                    var numberOfYears = timeSpan.TotalSeconds / TimeSpan.FromDays(365).TotalSeconds;
                    if (numberOfYears > annually)
                    {
                        annually += 1;
                        var account = _context.Accounts.Where(x => x.AccountNumber == bill.AccountNumber).FirstOrDefault();
                        account.Withdraw(bill.Amount);
                        account.AddTransaction(TransactionType.BillPay, bill.Amount, "PayeeID: " + bill.PayeeID.ToString());
                        await _context.SaveChangesAsync();
                    }
                }
                /*
                 * When you mark, pls uncomment the code below to run monthly bill every 10 seconds.
                 * You could refresh the transaction page to check the time stamp.
                 */
                //if (bill.Period.ToString().Equals("M") && bill.ScheduleDate < DateTime.UtcNow)
                //{
                //    var timeSpan = DateTime.UtcNow - bill.ScheduleDate;
                //    var numberOfTenSecdons = timeSpan.TotalSeconds / TimeSpan.FromSeconds(10).TotalSeconds;
                //    if (numberOfTenSecdons > everyTenSecond)
                //    {
                //        everyTenSecond += 1;
                //        var account = _context.Accounts.Where(x => x.AccountNumber == bill.AccountNumber).FirstOrDefault();
                //        account.Withdraw(bill.Amount);
                //        account.AddTransaction(TransactionType.BillPay, bill.Amount,"PayeeID: " + bill.PayeeID.ToString());
                //        await _context.SaveChangesAsync();
                //    }
                //}
            }
        }
    }
}
