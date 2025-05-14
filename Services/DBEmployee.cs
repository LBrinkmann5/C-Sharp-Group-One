using MySql.Data.MySqlClient;
namespace Southwest_Airlines.Services
{
    public class DBEmployee
    {
        private readonly string _connectionString;
        public DBEmployee(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<bool> UpdatePassAsync()
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    string query = "UPDATE employees SET password_hash = @password_hash WHERE LOWER(username) = LOWER(@username)";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", "TestUser");
                        command.Parameters.AddWithValue("@password_hash", BCrypt.Net.BCrypt.HashPassword("Password123"));
                        int rowsAffected = await command.ExecuteNonQueryAsync();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        

        public async Task<bool> VerifyLoginAsync(string username, string password)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    string query = "SELECT password_hash FROM employees WHERE LOWER(username) = LOWER(@username)";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                string hashedPass = reader.GetString(0);
                                Console.WriteLine(BCrypt.Net.BCrypt.HashPassword(password));
                                return BCrypt.Net.BCrypt.Verify(password, hashedPass);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return false;
        }
    }
}
