namespace Southwest_Airlines.Models.User
{
    public class FastPassPurchase
    {
        public int PurchaseId { get; set; }
        public double Price { get; set; }
        public int PassType { get; set; } = 0;
        public int Passengers { get; set; } = 1;
        public string? PaymentMethod { get; set; } = "Credit Card";
        public DateTime PurchaseDate { get; set; } = DateTime.Now;
        public int? UserId { get; set; }
        


    }
}
