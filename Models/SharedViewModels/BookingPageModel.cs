using Southwest_Airlines.Models.User;

namespace Southwest_Airlines.Models.SharedViewModels
{
    public class BookingPageModel : BaseViewModel
    {
        //public Flights Flights { get; set; } = new Flights();
        //public Airports Airport { get; set; }
        public Booking Booking { get; set; }
        public List<Flights> FlightsList { get; set; } = new List<Flights>();

    }
}
