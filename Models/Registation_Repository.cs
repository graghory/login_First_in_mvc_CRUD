using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Configuration;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Web.Http;

namespace login_First.Models
{
    public class Registation_Repository
    {
        private readonly string _connectionString;

        public Registation_Repository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
        [HttpPost]
        /*This is Create registation*/
        public Register CreateUser(Register model)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO login (id, Username, Password, First_Name, Last_Name, Email) VALUES (@id, @Username, @Password, @First_Name, @Last_Name, @Email)";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", model.id);
                    command.Parameters.AddWithValue("@Username", model.Username);
                    command.Parameters.AddWithValue("@Password", model.Password);
                    command.Parameters.AddWithValue("@First_Name", model.First_Name);
                    command.Parameters.AddWithValue("@last_Name", model.Last_Name);
                    command.Parameters.AddWithValue("@Email", model.Email);

                    int affectedRows = command.ExecuteNonQuery();
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }


        //This is a edit registation method
        public Register EditRegistation(Register model)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string query = "UPDATE login SET id = @id, First_Name = @First_Name, Last_Name = @Last_Name, Email = @Email, WHERE id = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@id", model.id);
                    cmd.Parameters.AddWithValue("@Username", model.Username);
                    cmd.Parameters.AddWithValue("@Password", model.Password);
                    cmd.Parameters.AddWithValue("@First_Name", model.First_Name);
                    cmd.Parameters.AddWithValue("@Last_Name", model.Last_Name);
                    cmd.Parameters.AddWithValue("@Email", model.Email);
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
            return null;
        }
    }
}