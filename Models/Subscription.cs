using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Subscription
    {
        public int Id { get; set; }
        public string UserId { get; set; } // Link to the user
        public string PlanName { get; set; }
        //public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }

        public string PaymentReference { get; set; }

    }
}
