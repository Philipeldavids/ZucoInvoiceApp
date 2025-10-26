using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class User : IdentityUser
    {
        [Key]
        
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string Password { get; set; }
       
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string ImageUrl { get; set; } = string.Empty;

        public string Company { get; set; } = string.Empty;

        public ICollection<Contact> Contacts { get; set; }


    }
}
