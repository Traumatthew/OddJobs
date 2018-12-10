using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OddJobs.Models
{
    public class CustomerWallet
    {
        [Key]
        public int Id { get; set; }
        public Double Price { get; set; }
        [Display(Name = "Cardholders Name")]
        public string CardHolderName { get; set; }
        [Display(Name = "Credit Card Number")]
        public string CreditCardNumber { get; set; }
        [Display(Name = "Expiration Date")]
        public string ExpirationDate { get; set; }
        [Display(Name = "CVV Number")]
        public string CVVNumber { get; set; }
    }
}