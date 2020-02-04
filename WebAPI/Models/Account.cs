using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public enum AccountType
    {
        Saving = 1,
        Checking = 2
    }
    public class Account
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required, StringLength(4, MinimumLength = 4)]
        [Display(Name = "Account Number")]
        public int AccountNumber { get; set; }

        [Required, StringLength(1)]
        [Display(Name = "Type")]
        public AccountType AccountType { get; set; }

        [Column(TypeName = "money")]
        [DataType(DataType.Currency)]
        public decimal Balance { get; set; }

        public virtual Customer Customer { get; set; }

        [Required]
        public int CustomerID { get; set; }

        [Required, StringLength(8)]
        public DateTime ModifyDate { get; set; }
        public virtual IList<Transaction> Transactions { get; set; }
        public virtual IList<BillPay> BillPays { get; set; }
        public void Deposit(decimal amount)
        {
            Balance += amount;
            ModifyDate = DateTime.UtcNow;
        }
        public void Withdraw(decimal amount)
        {
            Balance -= amount;
            ModifyDate = DateTime.UtcNow;
        }
        public void AddTransaction(TransactionType transactionType,decimal amount, string comment)
        {
            Transactions.Add(
                new Transaction
                {
                    TransactionType = transactionType,
                    Amount = amount,
                    AccountNumber = AccountNumber,
                    Comment = comment,
                    ModifyDate = DateTime.UtcNow
                });
        }
        public void AddTransaction(TransactionType transactionType,int destNumber, decimal amount, string comment)
        {
            Transactions.Add(
                new Transaction
                {
                    TransactionType = transactionType,
                    Amount = amount,
                    AccountNumber = AccountNumber,
                    DestinationAccountNumber = destNumber,
                    Comment = comment,
                    ModifyDate = DateTime.UtcNow
                }); 
        }
        public void AddTransaction(TransactionType transactionType, decimal amount)
        {
            Transactions.Add(
                new Transaction
                {
                    TransactionType = transactionType,
                    Amount = amount,
                    AccountNumber = AccountNumber,
                    ModifyDate = DateTime.UtcNow
                });
        }
    }
}
