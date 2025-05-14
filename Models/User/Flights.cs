namespace Southwest_Airlines.Models.User
{
    public class Flights
    {
        public int FlightId { get; set; }
        public string FlightNumber { get; set; } = "";
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public string Status { get; set; } = "";
        public string DepartureAirportCode { get; set; }
        public string ArrivalAirportCode { get; set; }

    }
}
