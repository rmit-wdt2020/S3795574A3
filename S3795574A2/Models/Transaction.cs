using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace S3795574A2.Models
{
    public enum TransactionType
    {
        /*
         * Deposit(D)
         * Withdraw(W)
         * Transfer(T)
         * Service Fee(S)
         * BillPay(B)
         */
        Deposit = 1,
        Withdraw = 2,
        Transfer = 3,
        ServiceFee = 4,
        BillPay = 5
    }
    public class Transaction
    {
        [Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionID { get; set; }

        [Required,StringLength(1)]
        public TransactionType TransactionType { get; set; }

        [Required, StringLength(4)]
        public int AccountNumber { get; set; }
        public virtual Account Account { get; set; }
        [ForeignKey("DestinationAccount"),StringLength(4)]
        [Display(Name = "Destination Account Number")]
        public int? DestinationAccountNumber { get; set; }
        public virtual Account DestinationAccount { get; set; }

        [Column(TypeName = "money")]
        [StringLength(8)]
        public decimal Amount { get; set; }

        [StringLength(255)]
        public string Comment { get; set; }
        [Required, StringLength(8)]
        public DateTime ModifyDate { get; set; }


    }
}
