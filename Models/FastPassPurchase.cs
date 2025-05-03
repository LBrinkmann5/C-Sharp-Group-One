namespace Southwest_Airlines.Models
{
    public class FastPassPurchase
    {
        string? PassType { get; set; }
        int? PassGuest { get; set; } = 1;
        int? PassPrice { get; set; } = 0;

    }
}
