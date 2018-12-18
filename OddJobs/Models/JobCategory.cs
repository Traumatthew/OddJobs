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
        [Display(Name = "Job Category")]
        public string CatName { get; set; }
    }

    //public enum JobCat
    //{
    //    Plumbing,
    //    Landscaping,
    //    Roofing,
    //    Cleaning,
    //    Electrical,
    //    GeneralLabor,
    //}
}