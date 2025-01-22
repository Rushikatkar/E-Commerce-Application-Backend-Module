using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Application_Backend_Module.Models.Entityes
{
    public class Catagory
    {
        [Key]
        public int CatagoryId { get; set; }
        public string CatagoryName { get; set; }
    }
}
