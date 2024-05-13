using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Category
{
    [Key]
    public int CategoryID { get; set; }

    [Required]
    [StringLength(255)]
    public string CategoryName { get; set; }

    public ICollection<Product> Products { get; set; }
}
