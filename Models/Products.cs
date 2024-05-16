using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Product
{
    [Key]
    public int ProductID { get; set; }

    [Required]
    [StringLength(255)]
    public string ProductName { get; set; }

    [Required]
    [Column(TypeName = "decimal(10, 2)")]
    public decimal Price { get; set; }

    [Required]
    public int Stock { get; set; }

    public string Description { get; set; }

    [ForeignKey("Category")]
    public int CategoryID { get; set; }
}
