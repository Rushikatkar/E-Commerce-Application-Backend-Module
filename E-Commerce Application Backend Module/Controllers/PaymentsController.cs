using E_Commerce_Backend_System.Models.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace E_Commerce_Backend_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
            AppDbContext _context;

        public PaymentsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("{orderId}")]
        public IActionResult ProcessPayment(string orderId)
        {
            var order = _context.Orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order == null) return NotFound();

            // Mock Payment Logic
            var paymentSuccess = new Random().Next(2) == 1;

            order.PaymentStatus = paymentSuccess ? "Success" : "Failed";
            _context.SaveChanges();

            return Ok(new { OrderId = orderId, PaymentStatus = order.PaymentStatus });
        }
    }
}
