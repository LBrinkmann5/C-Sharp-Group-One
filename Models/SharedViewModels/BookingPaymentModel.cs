using Southwest_Airlines.Models.User;

namespace Southwest_Airlines.Models.SharedViewModels
{
    public class BookingPaymentModel : BaseViewModel
    {
        public BookingPayment BookingPayment { get; set; }
        public Passengers Passengers { get; set; }

        //public FastPassPurchase fastPass { get; set; }
        public List<FastPassPurchase> fastPasses { get; set; }

        public int? SelectedFastPassId { get; set; }
        public int FlightId { get; set; }
        public double Price { get; set; } = 0.00;
        public bool UseFastPass { get; set; } = false;

    }
}
