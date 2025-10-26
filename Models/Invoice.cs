using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Invoice
    {
        
        [Key]        
        public int InvoiceID { get; set; }
        public int? InvoiceNumber { get; set; }
        public string? Client { get; set; }
        public string? CreatedDate { get; set; } 
       // public Status Status { get; set; }
        public string? ImageURl {  get; set; }
        public int? Tax { get; set; }

        public string? UserId { get; set; }

        public string? FootNote { get; set; }

        //private decimal totalPrice;
        public decimal? TotalPrice { get; set; }

        public List<Item> Items { get; set; } = new List<Item>();
    }
}
