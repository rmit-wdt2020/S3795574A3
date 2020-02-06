using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace S3795574A2.Models
{
    public enum Period
    {
        /*
         * Monthly (M),
         * Quarterly(Q),
         * Annually (Y),
         * Once Off (S),
         */
        M = 1,
        Q = 2,
        Y = 3,
        S = 4
    }
    public class BillPay
    {
        [Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BillPayID { get; set; }
        [Required]
        public int AccountNumber { get; set; }
        public virtual Account Account { get; set; }
        [Required]
        public int PayeeID { get; set; }
        public virtual Payee Payee { get; set; }
        [Column(TypeName = "money")]
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public DateTime ScheduleDate { get; set; }
        [Required]
        public Period Period { get; set; }
        [Required]
        public DateTime ModifyDate { get; set; }
        public bool IsLocked { get; set; } = false;
    }
}
