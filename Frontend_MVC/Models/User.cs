using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Frontend_MVC.Models
{
    public class UserModel
    {
        //public int UserID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        //public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }
}