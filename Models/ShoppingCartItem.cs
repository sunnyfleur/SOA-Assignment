using System.ComponentModel.DataAnnotations;

public class ShoppingCartItem
{
    [Key]
    public int ShoppingCartItemId { get; set; }

    [Required]
    public int ProductId { get; set; }

    [Required]
    public int Quantity { get; set; }

    public Product Product { get; set; }
}
