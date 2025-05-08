using System.ComponentModel.DataAnnotations;
namespace Southwest_Airlines.Models.User
{
    public class Payment
    {
        private double _price = 0;
        private int? _SelpassNum; // Default value for passenger number


        [Required(ErrorMessage = "* Please enter a valid card number.")]
        [LuhnValidatior]
        public string TBcardNumber { get; set; } = "";

        [Required(ErrorMessage = "* Please enter a valid security code.")]
        [RegularExpression(@"^\d{3,4}$", ErrorMessage = "* Security code must be 3 or 4 digits.")]
        public string TBsecurityCode { get; set; } = "";

        [Required(ErrorMessage = "* Please enter a valid expiration date.")]
        [RegularExpression(@"^(0[1-9]|1[0-2])\/?([0-9]{2})$", ErrorMessage = "* Expiration date must be in MM/YY format.")]
        public string TBexpiryDate { get; set; } = "";

        [Required(ErrorMessage = "* Please enter the cardholder's name.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "* Cardholder's name must contain only letters and spaces.")]
        public string TBcardName { get; set; } = "";
        [Required(ErrorMessage = "* Please select a country.")]
        public string Selcountry { get; set; } = "";
        [Required(ErrorMessage = "* Please enter a valid address.")]
        [RegularExpression(@"^[a-zA-Z0-9\s,.'-]{3,}$", ErrorMessage = "* Address must be at least 3 characters long and can contain letters, numbers, spaces, and certain punctuation.")]
        public string TBaddress { get; set; } = "";
        public string? TBoptionalAddress { get; set; }
        [Required(ErrorMessage = "* Please enter a valid city.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "* City must contain only letters and spaces.")]
        public string TBcity { get; set; } = "";
        [Required(ErrorMessage = "* Please enter a valid state.")]
        [RegularExpression(@"^[a-zA-Z\s]{2,}$", ErrorMessage = "* State must contain only letters and be at least 2 characters long.")]
        public string TBstate { get; set; } = "";
        [Required(ErrorMessage = "* Please enter a valid ZIP code.")]
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "* ZIP code must be in the format 12345 or 12345-6789.")]
        public string TBzip { get; set; } = "";
        [Required(ErrorMessage = "* Please enter a phone number.")]
        [RegularExpression(@"^[\d]{10,15}$", ErrorMessage = " * Please enter a valid phone number")]
        public string TBphone { get; set; } = "";
        [Required(ErrorMessage = "* Please select a passenger number.")]
        [Range (1, 9, ErrorMessage = "* Please select a passenger number.")]
        public int? SelpassNum 
        { get => _SelpassNum;
            set 
            {
                _SelpassNum = value;
            }
        }
        public int PassType { get; set; } = 0;
        public double Price 
        { 
            get => _price; 
            set 
            {
                if (value >= 0)
                {
                    _price = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Base price cannot be negative.");
                }
            } 
        } 
        

    }
}
