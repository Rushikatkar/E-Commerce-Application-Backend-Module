using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CURDAPI.Models
{
    [Table("UserTbl")]
    public class Users
    {
        [Key]
        public int UserID { get; set; }
       
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }



    }
}
