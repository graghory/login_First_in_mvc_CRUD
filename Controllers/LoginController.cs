using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;


namespace login_First.Controllers
{
    public class LoginController : Controller
    {
        string DefaultConnection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        // GET: Login
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        //this is a get a username and password
        [HttpPost]
        public ActionResult Login(string Username, string Password)
        {
            using (var connection = new MySqlConnection(DefaultConnection))
            {
                connection.Open();

                string query = "SELECT Username, Password FROM login WHERE Username = @Username";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", Username);


                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string storedPassword = reader["Password"].ToString();
                            if (storedPassword == Password)
                            {
                                    return RedirectToAction("Index", "Home");

                            }
                        }
                    }
                }
            }

            ViewBag.ErrorMessage = "Invalid login credentials";
            return View("Login");
        }

        //this is a registtion 
        [HttpPost]
        public ActionResult Register(string Username, string Password, string ConformPassword)
        {
            if (Password != ConformPassword)
            {

                ModelState.AddModelError("ConformPassword", "Passwords do not match.");
                return View();
            }
            using (MySqlConnection connection = new MySqlConnection(DefaultConnection))
            {
                connection.Open();
                string query = "INSERT INTO login (Username, Password) VALUES (@Username, @Password)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", Username);
                command.Parameters.AddWithValue("@Password", Password);
                int affectedRows = command.ExecuteNonQuery();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}