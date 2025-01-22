using E_Commerce_Backend_System.Models.Data;
using E_Commerce_Backend_System.Models.Entityes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace E_Commerce_Backend_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
         AppDbContext _context;

        public CartController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetCartItems()
        {
            var cartItems = _context.CartItems.Include(ci => ci.Product).ToList();
            return Ok(cartItems);
        }

        [HttpPost]
        public IActionResult AddToCart([FromBody] CartItem cartItem)
        {
            var product = _context.Products.Find(cartItem.ProductId);
            if (product == null || product.StockQuantity < cartItem.Quantity)
                return BadRequest("Insufficient stock");

            _context.CartItems.Add(cartItem);
            _context.SaveChanges();
            return Ok(cartItem);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCartItem(int id, [FromBody] CartItem cartItem)
        {
            var existingItem = _context.CartItems.Find(id);
            if (existingItem == null) return NotFound();

            existingItem.Quantity = cartItem.Quantity;
            _context.SaveChanges();
            return Ok(existingItem);
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveFromCart(int id)
        {
            var cartItem = _context.CartItems.Find(id);
            if (cartItem == null) return NotFound();

            _context.CartItems.Remove(cartItem);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
