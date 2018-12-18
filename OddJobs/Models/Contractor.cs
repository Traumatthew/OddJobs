using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OddJobs.Models
{
    public class Contractor
    {
        [Key]
        public int ContractorId { get; set; }
        [Display(Name = "Contractor Name")]
        public string ContractorName { get; set; }
        [Display(Name = "Phone")]
        public string ContractorPhone { get; set; }
        [Display(Name = "Area Of Expertise")]
        public string AreaOfExpertise { get; set; }
        [Display(Name = "Email")]
        public string ContractorEmail { get; set; }
        [Display(Name = "Street Address")]
        public string ContractorStreet { get; set; }
        [Display(Name = "State")]
        public string ContractorState { get; set; }
        [Display(Name = "City")]
        public string ContractorCity { get; set; }
        [Display(Name = "Zip Code")]
        public string ContractorZip { get; set; }

        public string TempRating { get; set; }

        public string RatingData { get; set; }

        public double? Rating { get; set; }

        public string lat { get; set; }

        public string lng { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    }
}