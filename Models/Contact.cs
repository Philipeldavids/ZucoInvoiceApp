using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Contact
    {
        [Key]
        public string ContactId { get; set; } = Guid.NewGuid().ToString();
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public Invoice? Invoice { get; set; }

        public string UserId { get; set; }
        public User? User { get; set; }
    }
}
