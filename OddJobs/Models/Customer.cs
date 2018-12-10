using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OddJobs.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Street { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string Zip { get; set; }

        public string lat { get; set; }

        public string lng { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("CustomerWallet")]
        public int? CustomerWalletId { get; set; }
        public CustomerWallet CustomerWallet { get; set; }

        public IEnumerable<Contractor> Contractors { get; set; }

        public IEnumerable<Job> Jobs { get; set; }
    }
}