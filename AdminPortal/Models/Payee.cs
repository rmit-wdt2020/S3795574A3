using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPortal.Models
{
    public class Payee:Person
    {
        [Required, DatabaseGenerated(DatabaseGeneratedOption.Identity), StringLength(4)]
        public int PayeeID { get; set; }
        public virtual IList<BillPay> BillPays { get; set; }
    }
}
