using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DTO
{
    public class UserResponseDTO
    {
        public string Id { get; set; }
        public string Email { get; set; }

        public string PhoneNumber { get; set; } 
        public DateTime CreatedAt { get; set; }

        public string Company { get; set; }
        public string ImageUrl { get; set; }
        public string Token { get; set; }
    }
}
