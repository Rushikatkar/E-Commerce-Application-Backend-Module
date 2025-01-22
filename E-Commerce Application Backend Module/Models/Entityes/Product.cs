using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_Backend_System.Models.Entityes
{
    [Table("ProductTbl")]
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string CatagoryName { get; set; }
        public int StockQuantity { get; set; }
        public string ProductPhotoPath { get; set; }
       
    }
}
