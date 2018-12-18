using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OddJobs.Models
{
    public class ContractorJobBid
    {
        [Key]
        public int BidId {get;set;}

        [Display(Name = "Estimated Hours For Compeletion")]
        public int? EstHoursToComplete { get; set; }

        [Display(Name = "Estimated Material Cost")]
        public int? MaterialCost { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name ="Date We Can Start")]
        public DateTime? Date { get; set; }

        public int? BidAmt { get; set; }

        [Required]
        [ForeignKey("Job")]
        public int JobId { get; set; }
        public Job Job { get; set; }

    //    [ForeignKey("Customers")]
    //    [Display(Name = "Customer Id")]
    //    public int CustomerId { get; set; }
    //    public Customer Customers { get; set; }

    //    [ForeignKey("Contractor")]
    //    [Display(Name = "Contractor Id")]
    //    public int? ContractorId { get; set; }
    //    public Contractor Contractor { get; set; }
    }
}