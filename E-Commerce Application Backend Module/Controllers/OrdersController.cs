using E_Commerce_Backend_System.Models.Data;
using E_Commerce_Backend_System.Models.Entityes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace E_Commerce_Backend_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
          AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult PlaceOrder([FromBody] Order order)
        {
            var cartItems = _context.CartItems.Include(ci => ci.Product).ToList();
            if (!cartItems.Any()) return BadRequest("Cart is empty");

            
            foreach (var item in cartItems)
            {
                var product = item.Product;
                if (product.StockQuantity < item.Quantity) return BadRequest("Insufficient stock");
                product.StockQuantity -= item.Quantity;
            }

         
            order.OrderId = Guid.NewGuid().ToString();
            order.OrderDate = DateTime.Now;
            order.Items = cartItems;
            order.PaymentStatus = "Pending";

            _context.Orders.Add(order);
            _context.CartItems.RemoveRange(cartItems);
            _context.SaveChanges();

            return Ok(order);
        }
    }
}
