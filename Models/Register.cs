using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace login_First.Models
{
    public class Register
    {
        public int id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
    }
}