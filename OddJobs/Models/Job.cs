﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OddJobs.Models
{
    public class Job
    {

        [Key]
        public int JobId { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        public string lat { get; set; }

        public string lng { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "A Date you wish you job to be started is required")]
        [Column(TypeName = "datetime2")]
        public DateTime? Date { get; set; }

        public bool Complete { get; set; }

        [Display(Name = "Job Description")]
        public string Details { get; set; }


        [ForeignKey("JobCategory")]
        [Display(Name = "What type of job is this?")]
        public int? CatId { get; set; }
        public JobCategory JobCategory { get; set; }

        public IEnumerable<JobCategory> JobCategories { get; set; }

        [ForeignKey("Customers")]
        public int? CustomerId { get; set; }
        public Customer Customers { get; set; }

        [ForeignKey("Contractor")]
        public int? ContractorId { get; set; }
        public Contractor Contractor { get; set; }
    }
}