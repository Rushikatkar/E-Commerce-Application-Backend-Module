using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_Backend_System.Models.Entityes
{
    [Table("OrderTbl")]
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public List<CartItem> Items { get; set; }
        public string BillingAddress { get; set; }
        public string ShippingAddress { get; set; }
        public string PaymentStatus { get; set; } 
    }
}
