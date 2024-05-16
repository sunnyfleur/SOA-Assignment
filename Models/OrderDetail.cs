using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SOA_Assignment.Models
{
    public class OrderDetail
    {
        [Key] public int OrderDetailID { get; set; }  
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        [JsonIgnore]
        public virtual Order? Order { get; set; } 
        public virtual Product? Product { get; set; }
    }
}
