using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OddJobs.Models
{
    public class JobCategory
    {
        [Key]
        public int CatId { get; set; }
        [Display(Name = "Job Type")]
        public string CatName { get; set; }
    }
}