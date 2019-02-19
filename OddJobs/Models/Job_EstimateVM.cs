using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OddJobs.Models
{
    public class Job_EstimateVM
    {
        public Customer customer { get; set; }

      
        //public int JobId { get; set; }
        public Job job { get; set; }

        public Contractor contractor { get; set; }

        public Estimate estimate { get; set; }

    }
}