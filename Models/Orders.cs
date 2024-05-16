using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOA_Assignment.Models
{
    public class Orders
    {
        [Key]
        public int OrderID { get; set; }

        [Required]
        public int CustomerID { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal TotalAmount { get; set; }

        [StringLength(100)]
        public string Status { get; set; }

        // Navigation property to represent the relationship with Customer
        [ForeignKey("CustomerID")]
        public virtual Customer Customer { get; set; }

        // Navigation property to represent the collection of order items
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
