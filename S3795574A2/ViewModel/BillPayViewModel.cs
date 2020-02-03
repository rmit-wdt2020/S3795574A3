using S3795574A2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using S3795574A2.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace S3795574A2.ViewModel
{
    public class BillPayViewModel
    {
        public int BillPayId { get; set; }
        public int Account { get; set; }
        public int Payee { get; set; }
        [Range(0.01, int.MaxValue, ErrorMessage = "Price must be greater than or equal than 0.01.")]
        public decimal Amount { get; set; }
        public DateTime ScheduledDate { get; set; }
        public string Period { get; set; }
        public async Task CreateBillPay(NwbaContext nwbaContext)
        {     
            var billPay = new BillPay
            {
                AccountNumber = Account,
                PayeeID = Payee,
                Amount = Amount,
                ScheduleDate = ScheduledDate.ToUniversalTime(),
                ModifyDate = DateTime.UtcNow
            };
            if (Period.Equals("S"))
                billPay.Period = Models.Period.S;             
            if (Period.Equals("M"))
                billPay.Period = Models.Period.M;
            if (Period.Equals("Q"))
                billPay.Period = Models.Period.Q;
            if (Period.Equals("Y"))
                billPay.Period = Models.Period.Y;

            nwbaContext.BillPays.Add(billPay);
            await nwbaContext.SaveChangesAsync();
        }
    }
}
