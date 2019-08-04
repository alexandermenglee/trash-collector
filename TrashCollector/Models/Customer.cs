using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }
        public string PickUpDay { get; set; }
        public double BillAmount { get; set; }
        public DateTime? SpecialPickupDate { get; set; } = null;
        public DateTime? StartSuspension { get; set; } = null;
        public DateTime? EndSuspendsion { get; set; } = null;
        public bool PickedUp { get; set; }
        public bool AccountSuspended { get; set; } = false;
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}