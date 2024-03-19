using login_First.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using MySql.Data.MySqlClient;
using System.Web.Http;
using System.Web.UI.WebControls;
using Google.Protobuf.Reflection;
using Newtonsoft.Json.Linq;

namespace login_First.Controllers
{
    public class RegisterController : ApiController
    {
        private readonly IConfiguration _configuration;

        public RegisterController()
        {
            
        }
        public RegisterController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string create(Register Register)
        {
            MySqlConnection con = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            MySqlCommand cmd = new MySqlCommand("INSERT INTO login (id, Username, Password, First_Name, Last_Name, Email) VALUES (@id, @Username, @Password, @First_Name, @Last_Name, @Email)");
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i>0) 
            {
                return "data insertede";
            }
            else
            {
                return "Error";
            }
        }
    }
}
