using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class ItemDTO
    {
       
        public string Description { get; set; }
        public int Quantity { get; set; }
        public float Discount { get; set; }
        public float UnitPrice { get; set;}
        public float Amount { get; set; }
        
    }
}
