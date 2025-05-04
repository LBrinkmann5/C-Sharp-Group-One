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
    $("#TBphone").on("input", (evt) => {
        restrictCharacters(evt);
    });

    const isValidLength = (length) => {
        return length === 13 || length === 15 || length === 16 || length === 19;
    };

    const isValid = (cardNumber) => {
    
        let sum = 0;
        let length = cardNumber.length;

        //if (!isValidLength(length))
        //{
        //    console.log("Invalid length");
        //    return false;
        //}
        

        let isSecond = false;
        for (let i = length - 1; i >= 0; i--)
        {
            let d = cardNumber[i] - '0';
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
         }
    });
    $("#TBsecurityCode").on("input", (evt) => {
        restrictCharacters(evt);
        if (evt.target.value.length > 4) {
            evt.target.value = evt.target.value.slice(0, 4); // Limit to 4 digits
        }
    });

    $("#TBexpiryDate").on("keydown", (evt) =>
    {
        const input = evt.target;
        const cursorPosition = input.selectionStart;

        const key = evt.key;
        if (key === "Backspace" && cursorPosition > 0 && input.value[cursorPosition - 1] === "/")
        {
            evt.preventDefault(); // Prevent the default backspace behavior
            input.value = input.value.slice(0, cursorPosition - 1) + input.value.slice(cursorPosition); // Remove the slash
            input.setSelectionRange(cursorPosition - 1, cursorPosition - 1); // Move the cursor back
        }

    });

    $("#TBexpiryDate").on("input", (evt) => {
        restrictCharacters(evt);
        const input = evt.target;

        if (input.value.length > 2 && input.value.indexOf("/") === -1) {
            input.value = input.value.slice(0, 2) + "/" + input.value.slice(2); // Insert slash after MM
        }

        if (input.value.length > 5) {
            input.value = input.value.slice(0, 5); // Limit to MM/YY format
        }
    })

    //To change to cost of the product
    const priceElement = $(".fast-pass-price");
    const basePrice = parseFloat($("#basePrice").val()) || 0;
    const price = $("#price");
    $("#SelpassNum").on("change", function ()
    {
        const selectedValue = parseInt($(this).val()) || 4;
        const totalPrice = basePrice + (selectedValue - 4) * 10;
        price.val(totalPrice.toFixed(2)); // Update the hidden input value
        priceElement.text(`$${totalPrice.toFixed(2)}`);
    });

});