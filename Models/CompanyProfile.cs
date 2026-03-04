using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CompanyProfile
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Website { get; set; } = default!;
        public string? LogoUrl { get; set; }

        public Guid UserId { get; set; }  // Owner of the settings
    }
}
