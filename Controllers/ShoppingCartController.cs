using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SOA_Assignment.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOA_Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly YourDbContext _context;

        public ShoppingCartController(YourDbContext context)
        {
            _context = context;
        }

        // GET: api/ShoppingCart
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShoppingCartItem>>> GetShoppingCartItems()
        {
            return await _context.ShoppingCartItems.ToListAsync();
        }

        // GET: api/ShoppingCart/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShoppingCartItem>> GetShoppingCartItem(int id)
        {
            var shoppingCartItem = await _context.ShoppingCartItems.FindAsync(id);

            if (shoppingCartItem == null)
            {
                return NotFound();
            }

            return shoppingCartItem;
        }

        // POST: api/ShoppingCart
        [HttpPost]
        public async Task<ActionResult<ShoppingCartItem>> PostShoppingCartItem(ShoppingCartItem shoppingCartItem)
        {
            _context.ShoppingCartItems.Add(shoppingCartItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShoppingCartItem", new { id = shoppingCartItem.ShoppingCartItemId }, shoppingCartItem);
        }

        // PUT: api/ShoppingCart/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShoppingCartItem(int id, ShoppingCartItem shoppingCartItem)
        {
            if (id != shoppingCartItem.ShoppingCartItemId)
            {
                return BadRequest();
            }

            _context.Entry(shoppingCartItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoppingCartItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/ShoppingCart/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShoppingCartItem(int id)
        {
            var shoppingCartItem = await _context.ShoppingCartItems.FindAsync(id);
            if (shoppingCartItem == null)
            {
                return NotFound();
            }

            _context.ShoppingCartItems.Remove(shoppingCartItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShoppingCartItemExists(int id)
        {
            return _context.ShoppingCartItems.Any(e => e.ShoppingCartItemId == id);
        }
        // GET: api/ShoppingCart/Total
        [HttpGet("Total")]
        public async Task<ActionResult<decimal>> GetTotal()
        {
            decimal total = await _context.ShoppingCartItems.SumAsync(item => item.Quantity * item.Product.Price);
            return total;
        }

        // POST: api/ShoppingCart/AddToCart
        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                return NotFound("Product not found.");
            }

            var existingItem = await _context.ShoppingCartItems.FirstOrDefaultAsync(item => item.ProductId == productId);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
                await _context.SaveChangesAsync();
                return Ok("Quantity updated in the shopping cart.");
            }

            var newItem = new ShoppingCartItem { ProductId = productId, Quantity = quantity };
            _context.ShoppingCartItems.Add(newItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShoppingCartItem", new { id = newItem.ShoppingCartItemId }, newItem);
        }

        // PUT: api/ShoppingCart/UpdateQuantity/5
        [HttpPut("UpdateQuantity/{id}")]
        public async Task<IActionResult> UpdateQuantity(int id, int quantity)
        {
            var shoppingCartItem = await _context.ShoppingCartItems.FindAsync(id);
            if (shoppingCartItem == null)
            {
                return NotFound("Shopping cart item not found.");
            }

            shoppingCartItem.Quantity = quantity;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/ShoppingCart/ClearCart
        [HttpDelete("ClearCart")]
        public async Task<IActionResult> ClearCart()
        {
            _context.ShoppingCartItems.RemoveRange(_context.ShoppingCartItems);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
