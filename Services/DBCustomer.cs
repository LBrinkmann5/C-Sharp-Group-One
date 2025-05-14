using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using Southwest_Airlines.Models.User;
using System.Security.Cryptography.X509Certificates;

namespace Southwest_Airlines.Services
{
    public class DBCustomer
    {
        private readonly string _connectionString;
        public DBCustomer(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        ////To Test Console Output
        //public async Task<List<string>> GetDataAsync()
        //{
        //    var results = new List<string>();
        //    using (var connection = new MySqlConnection(_connectionString))
        //    {
        //        await connection.OpenAsync();
        //        string query = "SELECT username FROM users";
        //        using ( var command = new MySqlCommand(query, connection))
        //        {
        //            using (var reader = await command.ExecuteReaderAsync())
        //            {
        //                while (await reader.ReadAsync())
        //                {
        //                    string username = reader.GetString(0);
        //                    results.Add(username);
        //                    Console.WriteLine(results.Count());
        //                }
        //            }
        //        }
        //    }
        //    return results;
        //}
        //To Login
        public async Task<bool> VerifyLoginAsync(string username, string password)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    string query = "SELECT password_hash FROM users WHERE LOWER(username) = LOWER(@username)";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                string hashedPass = reader.GetString(0);
                                return BCrypt.Net.BCrypt.Verify(password, hashedPass);
                            }
                        }
                    }
                }
                return false;
            }
            catch (MySqlException ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"Error during login verification: {ex.Message}");
                return false;
            }

        }
        //To Register
        public async Task<bool> RegisterUserAsync(Registration registration)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                try
                {
                    string query = @"INSERT INTO users (username, first_name, last_name, email, password_hash, home_address,  phone_number  ) VALUES (@username, @first_name, @last_name, @email, @password_hash, @home_address,  @phone_number)";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        Console.WriteLine($"Last Name: {registration.TBlname}");
                        Console.WriteLine($"Last Name Length: {registration.TBlname?.Length}");
                        command.Parameters.AddWithValue("@username", registration.TBuser);
                        command.Parameters.AddWithValue("@password_hash", BCrypt.Net.BCrypt.HashPassword(registration.TBpass));
                        command.Parameters.AddWithValue("@email", registration.TBemail);
                        command.Parameters.AddWithValue("@phone_number", registration.TBphone);
                        command.Parameters.AddWithValue("@home_address", registration.TBaddress);
                        command.Parameters.AddWithValue("@first_name", registration.TBfname);
                        command.Parameters.AddWithValue("@last_name", registration.TBlname?.Trim());
                        int rowsAffected = await command.ExecuteNonQueryAsync();
                        return rowsAffected > 0;
                    }
                }
                catch (MySqlException ex)
                {
                    // Log the exception or handle it as needed
                    Console.WriteLine($"Error during registration: {ex.Message}");
                    return false;
                }
            }
        }
        //To Get User First Name
        public async Task<string?> GetUserFirstNameAsync(string username)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    string query = "SELECT first_name FROM users WHERE LOWER(username) = LOWER(@username)";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return reader.GetString(0);
                            }
                        }
                    }
                }
                return null;
            }
            catch (MySqlException ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"Error during user retrieval: {ex.Message}");
                return null;
            }
        }
        //Purchase Pass
        public async Task<bool> PurchasePassAsync(FastPassPurchase fastPassPurchase)
        {
            try
            {
                using(var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    string query = @"INSERT INTO fastpasspurchases(user_id, purchase_date, price, payment_method, pass_type, passengers) VALUES (@user_id, @purchase_date, @price, @payment_method, @pass_type, @passengers)";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@user_id", fastPassPurchase.UserId);
                        command.Parameters.AddWithValue("@purchase_date", fastPassPurchase.PurchaseDate);
                        command.Parameters.AddWithValue("@price", fastPassPurchase.Price);
                        command.Parameters.AddWithValue("@payment_method", fastPassPurchase.PaymentMethod);    
                        command.Parameters.AddWithValue("@pass_type", fastPassPurchase.PassType);
                        command.Parameters.AddWithValue("@passengers", fastPassPurchase.Passengers);
                        int rowsAffected = await command.ExecuteNonQueryAsync();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (MySqlException ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"Error during pass purchase: {ex.Message}");
                return false;
            }
        }
        //Get User ID
        public async Task<int> GetUserIdAsync(string username)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    string query = "SELECT user_id FROM users WHERE LOWER(username) = LOWER(@username)";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return reader.GetInt32(0);
                            }
                        }
                    }
                }
                return -1; // User not found
            }
            catch (MySqlException ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"Error during user ID retrieval: {ex.Message}");
                return -1;
            }
        }
        public async Task<List<FastPassPurchase>> GetUserPurchasesAsync(int userId)
        {
            var purchases = new List<FastPassPurchase>();
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    string query = "SELECT purchase_id, purchase_date, price, payment_method, pass_type, passengers FROM fastpasspurchases WHERE user_id = @user_id";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@user_id", userId);
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var purchase = new FastPassPurchase
                                {
                                    PurchaseId = reader.GetInt32(0),
                                    PurchaseDate = reader.GetDateTime(1),
                                    Price = reader.GetDouble(2),
                                    PaymentMethod = reader.GetString(3),
                                    PassType = reader.GetInt32(4),
                                    Passengers = reader.GetInt32(5)
                                };
                                purchases.Add(purchase);
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"Error during purchase retrieval: {ex.Message}");
            }
            return purchases;
        }
        //Search Airports
        public async Task<List<Airports>> SearchAirportsAsync(string term)
        {
            var airports = new List<Airports>();
            string codeTerm = term.ToUpperInvariant();
            string likeTerm = "%" + term + "%";
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    string query = @"SELECT airport_code, airport_name, city, state FROM airports 
                        WHERE airport_code = @codeTerm
                        OR airport_name LIKE @likeTerm
                        OR city LIKE @likeTerm 
                        OR state LIKE @likeTerm";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@codeTerm", codeTerm);
                        command.Parameters.AddWithValue("@likeTerm", likeTerm);
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                airports.Add(new Airports
                                {
                                    Code = reader.GetString(0),
                                    Name = reader.GetString(1),
                                    City = reader.GetString(2),
                                    State = reader.GetString(3)
                                });
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"Error during airport search: {ex.Message}");
            }
            return airports;
        }
        //Get Airport ID
        public async Task<int> GetAirportIdAsync(string airportCode)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    string query = "SELECT airport_id FROM airports WHERE LOWER(airport_code) = LOWER(@airport_code)";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@airport_code", airportCode);
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return reader.GetInt32(0);
                            }
                        }
                    }
                }
                return -1; // Airport not found
            }
            catch (MySqlException ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"Error during airport ID retrieval: {ex.Message}");
                return -1;
            }
        }
        //Get Flights by joining with Airports
        public async Task<List<Flights>> GetFlightsAsync(DateTime departureDate, string departCode,string arriveCode)
        {
            var flights = new List<Flights>();
            int derpartId = await GetAirportIdAsync(departCode);
            int arriveId = await GetAirportIdAsync(arriveCode);
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    string query = @"SELECT flight_id, flight_number, departure_time, arrival_time, status
                    FROM flightschedules
                    WHERE departure_airport_id = @departId
                    AND arrival_airport_id = @arriveId
                    AND DATE(departure_time) = @departureDate
                    ORDER BY departure_time";
                    using(var command = new MySqlCommand(query, connection)) {
                        command.Parameters.AddWithValue("@departId", derpartId);
                        command.Parameters.AddWithValue("@arriveId", arriveId);
                        command.Parameters.AddWithValue("@departureDate", departureDate.Date);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                flights.Add(new Flights
                                {
                                    FlightId = reader.GetInt32(0),
                                    FlightNumber = reader.GetString(1),
                                    DepartureTime = reader.GetDateTime(2),
                                    ArrivalTime = reader.GetDateTime(3),
                                    Status = reader.GetString(4),
                                    DepartureAirportCode = departCode,
                                    ArrivalAirportCode = arriveCode

                                });
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"Error during flight retrieval: {ex.Message}");
            }
            return flights;
        }
    }
}
