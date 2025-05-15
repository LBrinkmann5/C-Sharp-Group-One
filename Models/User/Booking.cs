namespace Southwest_Airlines.Models.User
{
    public class Booking
    {
        public string DepartCode { get; set; }
        public string ArriveCode { get; set; }
        public DateTime DepartDate { get; set; }
        public int PassengerCount { get; set; }

    }
}
