namespace Southwest_Airlines.Models.User
{
    public class FastPassPurchase
    {
        public int PurchaseId { get; set; }
        public double Price { get; set; }
        public string? PaymentMethod { get; set; } = "Credit Card";
        public DateTime PurchaseDate { get; set; } = DateTime.Now;
        public int? UserId { get; set; }
        


    }
}
