using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class InvoiceDTO
    {

        public string InvoiceNumber { get; set; }
        public string? ContactName { get; set; } 
        

        public IFormFile? Image { get; set; }
        //public Status Status { get; set; }
        public int? Tax { get; set; }

        public string? FootNote { get; set; }
        public decimal? TotalPrice { get; set; }
        public string? Items { get; set; }

        public string? UserId { get; set; }
    }
}
