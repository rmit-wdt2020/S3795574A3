using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminPortal.Models
{
    public abstract class Person
    {
        [Required(AllowEmptyStrings = false), StringLength(50, MinimumLength = 8)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Address { get; set; }
        [StringLength(40)]
        public string City { get; set; }
        [StringLength(3)]
        public String States { get; set; }
        [StringLength(10)]
        [RegularExpression("[0-9]{4}", ErrorMessage = "Postcode should be 4 digits")]
        public string PostCode { get; set; }
        [Required, StringLength(15)]
        [RegularExpression("^(61-)[0-9]{8}", ErrorMessage = "Phone Number should follow the pattern 61-xxxxxxxx")]
        public string Phone { get; set; }
    }

    public enum States
    {
        NSW = 1,
        QLD = 2,
        VIC = 3,
        SA = 4,
        WA = 5,
        NT = 6,
        TAS = 7,
        ACT = 8
    }

    public class Customer :Person
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int CustomerID { get; set; }

        [StringLength(11, MinimumLength = 11 , ErrorMessage = "TFN should be 11 digits.")]
        [RegularExpression("[0-9]{11}", ErrorMessage = "TFN should be a number.")]
        public string TFN { get; set; }

        public virtual IList<Account> Accounts { get; set; }
    }
}
