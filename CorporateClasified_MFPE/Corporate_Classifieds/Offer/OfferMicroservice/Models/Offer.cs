using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OfferMicroservice.Models
{
    public class Offer
    {
        
        public int OfferId { get; set; }

        public int EmployeeId { get; set; }

        public string Status { get; set; }

        public int Likes { get; set; }

        public string Category { get; set; }

        public string Details { get; set; }

        public DateTime OpenedDate { get; set; }

        public DateTime EngagedDate { get; set; }

        public DateTime ClosedDate { get; set; }

    }
}
