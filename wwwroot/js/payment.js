"use strict"
$(() =>
{
    console.log("Payment.js loaded");
    const restrictCharacters = (evt) => {
        const validChars = /^[\d]+$/; // Valid characters
        evt.target.value = evt.target.value.replace(/\D/g, "");
        //if (!validChars.test(evt.key)) {
        //    evt.preventDefault(); // Prevent the character from being entered
        //}
    }
    //Prevents the user from entering anything other than numbers
    $("#TBphone").on("keypress", (evt) => {
        restrictCharacters(evt);
    });

    const isValidLength = (length) => {
        return length === 13 || length === 15 || length === 16 || length === 19;
    };

    const isValid = (cardNumber) => {
    
        let sum = 0;
        let length = cardNumber.length;

        if (!isValidLength(length))
        {
            console.log("Invalid length");
            return false;
        }
        

        let isSecond = false;
        for (let i = length - 1; i >= 0; i--)
        {
            let d = cardNumber[i].charCodeAt() - '0';
            if (isSecond) {
                d = d * 2;
                if (d > 9) {
                    d -= 9;
                }
                
            }
            sum += d;
            isSecond = !isSecond;
        }
        console.log(sum);
        console.log(sum % 10);
        return (sum % 10 == 0);
    };

    $("#TBcardNumber").on("input", (evt) => {
        restrictCharacters(evt);
        let cardNumber = evt.target.value.replace(/\s/g, ""); // Remove spaces
        if (isValid(cardNumber)) {
            $("#TBcardNumber").next().text(""); // Clear any previous error message
            console.log("Valid card number entered: " + cardNumber);
        }
        else {
            $("#TBcardNumber").next().text("* Invalid card number.");
            console.log("Invalid card number entered: " + cardNumber);
            $("#TBcardNumber").next().css("color", "red");
         }
    });
    $("#TBsecurityCode").on("keypress", (evt) => {
        restrictCharacters(evt);
    });

    //To change to cost of the product
    const priceElement = $(".fast-pass-price");
    const basePrice = parseFloat($("#basePrice").val()) || 0;
    $("#SelpassNum").on("change", function () {
        const selectedValue = parseInt($(this).val()) || 4;
        const totalPrice = basePrice + (selectedValue - 4) * 10;
        priceElement.text(`$${totalPrice.toFixed(2)}`);
    });

});