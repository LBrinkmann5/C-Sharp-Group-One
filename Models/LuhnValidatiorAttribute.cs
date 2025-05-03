using System.ComponentModel.DataAnnotations;
namespace Southwest_Airlines.Models
{
    public class LuhnValidatiorAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return new ValidationResult("* Credit card number is required.");
            }

            string cardNumber = value.ToString()!.Replace(" ", ""); // Remove spaces

            if (!IsValidLuhn(cardNumber))
            {
                return new ValidationResult("* Invalid credit card number.");
            }

            return ValidationResult.Success;
        }

        private bool IsValidLuhn(string cardNumber)
        {
            int sum = 0;
            bool isSecond = false;

            for (int i = cardNumber.Length - 1; i >= 0; i--)
            {
                if (!char.IsDigit(cardNumber[i]))
                {
                    return false; // Invalid character
                }

                int digit = cardNumber[i] - '0';

                if (isSecond)
                {
                    digit *= 2;
                    if (digit > 9)
                    {
                        digit -= 9;
                    }
                }

                sum += digit;
                isSecond = !isSecond;
            }

            return (sum % 10 == 0);
        }
    }
}
