using Microsoft.AspNetCore.Mvc.Rendering;
using S3795574A2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace S3795574A2.ViewModel
{
    public class TransactionViewModel
    {
        public const decimal WithdrawFee = 0.1m;
        public const decimal TransferFee = 0.2m;
        public int AccountNumber { get; set; }
        public int DestinationAccountNumber { get; set; }
        public List<SelectListItem> TransactionTypes { get; set; }
        public int SelecteID { get; set; }
        [Range(0.01, int.MaxValue, ErrorMessage = "Price must be greater than or equal than 0.01.")]
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public AccountType AccountType { get; set; }
        public string Comment { get; set; }
        public TransactionViewModel()
        {
            TransactionTypes = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Deposit",
                    Value = "1"
                },
                new SelectListItem
                {
                    Text = "Withdraw",
                    Value = "2"
                },
                new SelectListItem
                {
                    Text = "Transfer",
                    Value = "3"
                }
            };
        }
        public bool CheckBalance()
        {
            if (AccountType.ToString().Equals("Saving"))
            {
                return Balance > (Amount + TransferFee);
            }
            if (AccountType.ToString().Equals("Checking"))
            {
                return Balance > (Amount + 200 + TransferFee);
            }
            return false;
        }
    }
}
