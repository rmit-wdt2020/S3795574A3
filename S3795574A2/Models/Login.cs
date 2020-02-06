using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace S3795574A2.Models
{
    public class Login
    {
        [Required,StringLength(8, MinimumLength = 8 ,ErrorMessage = "UseID should be 8 digits.")]
        [Key]
        public string UserID{ get; set; }

        [Required]
        public int CustomerID { get; set; }

        public virtual Customer Customer { get; set; }


        [Required, StringLength(64)]
        public string PasswordHash { get; set; }

        [Required]
        public DateTime ModifyDate { get; set; }
        public int Attempt { get; set; }
        public bool IsLocked { get; set; } = false;
        public DateTime LockedToDate { get; set; }
    }
}
