using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SOA_Assignment.Models
{
    public class Order
    {
        [Key] public int OrderID { get; set; } 
        public int CustomerID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }

        [JsonIgnore]
        public virtual Customers? Customer { get; set; }  
        public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
