using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using Southwest_Airlines.Models;

namespace Southwest_Airlines.Services
{
    public class Test
    {
        private readonly string _connectionString;
        public Test(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        //To Test Console Output
        public async Task<List<string>> GetDataAsync()
        {
            var results = new List<string>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT username FROM users";
                using ( var command = new MySqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            string username = reader.GetString(0);
                            results.Add(username);
                            Console.WriteLine(results.Count());
                        }
                    }
                }
            }
            return results;
        }
        //To Login
        public async Task<bool> VerifyLoginAsync(string username, string password)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT password_hash FROM users WHERE username = @username";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    using (var reader = await command.ExecuteReaderAsync())
                    { if (await reader.ReadAsync())
                        {
                            string hashedPass = reader.GetString(0);
                            return BCrypt.Net.BCrypt.Verify(password, hashedPass);
                        }
                    }
                }
            }
            return false;

        }
        //To Register
        public async Task<bool> RegisterUserAsync(Registration registration)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = @"INSERT INTO users (username, password_hash, phone_number, email, home_address) VALUES (@username, @password_hash, @email, @phone_number, @home_address)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", registration.TBuser);
                    command.Parameters.AddWithValue("@password_hash", BCrypt.Net.BCrypt.HashPassword(registration.TBpass));
                    command.Parameters.AddWithValue("@email", registration.TBemail);
                    command.Parameters.AddWithValue("@phone_number", registration.TBphone);
                    command.Parameters.AddWithValue("@home_address", registration.TBaddress);
                    //command.Parameters.AddWithValue("@fname", registration.TBfname);
                    //command.Parameters.AddWithValue("@lname", registration.TBlname);
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
