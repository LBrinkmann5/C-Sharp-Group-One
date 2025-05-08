namespace Southwest_Airlines.Services
{
    public class DBEmployee
    {
        private readonly string _connectionString;
        public DBEmployee(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
    }
}
