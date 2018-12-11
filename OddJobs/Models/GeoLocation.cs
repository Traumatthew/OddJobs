using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OddJobs.Models
{
    public class GeoLocation
    {
        public ApplicationDbContext db = new ApplicationDbContext();
        [Key]
        public int GeoID { get; set; }
        [ForeignKey("Customers")]
        [Display(Name = "Customer Id")]
        public int CustomerId { get; set; }
        public Customer Customers { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }

        public string key = Keys.GoogleKey;
        public string srcKey = "https://maps.googleapis.com/maps/api/js?libraries=places&key=" + Keys.GoogleKey + "&callback=initMap";

        public dynamic GetLatandLong(Customer cust)
        {
            string url = "https://maps.googleapis.com/maps/api/geocode/json?address=" + cust.Street.Replace(" ", "+") + cust.City.Replace(" ", "+") + ",+" + cust.State + ",+&key=" + Keys.GoogleKey;
            var result = new System.Net.WebClient().DownloadString(url);
            var items = JsonConvert.DeserializeObject<dynamic>(result);
            return items.results[0].geometry.location;
        }

        public dynamic GetLatandLong(Contractor cont)
        {
            string url = "https://maps.googleapis.com/maps/api/geocode/json?address=" + cont.ContractorStreet.Replace(" ", "+") + cont.ContractorCity.Replace(" ", "+") + ",+" + cont.ContractorState + ",+&key=" + Keys.GoogleKey;
            var result = new System.Net.WebClient().DownloadString(url);
            var items = JsonConvert.DeserializeObject<dynamic>(result);
            return items.results[0].geometry.location;
        }

        public dynamic GetLatandLong(Job job)
        {
            string url = "https://maps.googleapis.com/maps/api/geocode/json?address=" + job.Street.Replace(" ", "+") + job.City.Replace(" ", "+") + ",+" + job.State + ",+&key=" + Keys.GoogleKey;
            var result = new System.Net.WebClient().DownloadString(url);
            var items = JsonConvert.DeserializeObject<dynamic>(result);
            return items.results[0].geometry.location;
        }


    }
}