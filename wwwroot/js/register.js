"use strict";
$(document).ready(() => 
{
    const passwordvalid = /^(?=.*\d)(?=.*[A-Z])(?=.*[!@#$%^&*()<>{}[\]-_=+,.?":])(?!.*\s\s).{10,}$/
    const emailvalid = /^((?!\.)[\w\-_.]*[^.])(@\w+)(\.\w+(\.\w+)?[^.\W])$/
    const phonevalid = /^[\d]{10,15}$/
    
    const restrictCharacters = (evt) => {
        const validChars = /^[\d]+$/; // Valid characters
            if (!validChars.test(evt.key)) 
            {
                evt.preventDefault(); // Prevent the character from being entered
            }
    }
    //Prevents the user from entering anything other than numbers
    $("#TBphone").on("keypress", (evt) =>{
        restrictCharacters(evt);
    });

    //Show and Hide Password
    $("#btnShow").on("click", function(){
        const passwordField = $("#TBpass");
        const buttonIcon = $("#btnShow");
        const fieldType = passwordField.attr("type");

        if (fieldType === "password"){
            passwordField.attr("type", "text");
            buttonIcon.attr("class", "btn btnShow bi-eye-slash-fill" );
        }
        else {
            passwordField.attr("type", "password");
            buttonIcon.attr("class", "btn btnShow bi-eye-fill" );
        }


    });
    //Submit form
    /*$("#customer_register").submit( evt =>
    {
        const username = $("#TBuser").val().trim();
        const password = $("#TBpass").val().trim();
        const email = $("#TBemail").val().trim();
        const firstName = $("#TBfname").val().trim();
        const lastName = $("#TBlname").val().trim();
        const phone = $("#TBphone").val().trim();
        const address = $("#TBaddress").val().trim();

        //Basic username validation
        /*if(username == "")
        {
            $("#TBuser").next().text("* Username field can not be left empty.");
            $("#TBuser").next().css("color","red");
            isValid = false;
        }
        else 
        {
            $("#TBuser").next().text("");
            $("#TBuser").next().css("color","#636363");
        }
        //Basic password validation
        if(password == ""){
            $("#passError").text("* Password field can not be left empty.");
            $("#passError").css("color","red");
            isValid = false;
        }
        else if(!passwordvalid.test(password))
        {
            $("#passError").text("* Password must be at least 10 characters long, contain a capital letter, a number, and a special character.");
            $("#passError").css("color","red");
            isValid = false;
        }
        else 
        {
            $("#passError").text("");
            $("#passError").css("color", "#636363");
        }
        //Basic Email Validation
        if(email == ""){
            $("#TBemail").next().text("* Email field can not be left empty.");
            $("#TBemail").next().css("color", "red");
            isValid = false;
        }
        else if(!emailvalid.test(email)){
            $("#TBemail").next().text("* Email is not valid.");
            $("#TBemail").next().css("color", "red");
            isValid = false;
        }
        else{
            $("#TBemail").next().text("");
            $("#TBemail").next().css("color", "#636363");
        }
        //Basic Name Validation
        if(firstName == ""){
            $("#TBfname").next().text("* First name field can not be left empty.");
            $("#TBfname").next().css("color", "red");
            isValid = false;
        }
        else 
        {
            $("#TBfname").next().text("");
            $("#TBfname").next().css("color", "#636363");
        }
        if(lastName == ""){
            $("#TBlname").next().text("* Last name field can not be left empty.");
            $("#TBlname").next().css("color", "red");
            isValid = false;
        }
        else
        {
            $("#TBlname").next().text("");
            $("#TBlname").next().css("color", "#636363");
        }
        //Basic Phone Validation
        if(phone == ""){
            $("#TBphone").next().text("* Phone number field can not be left empty.");
            $("#TBphone").next().css("color", "red");
            isValid = false;
        }
        else if(!phonevalid.test(phone)){
            $("#TBphone").next().text("* Phone number is too large or too short.");
            $("#TBphone").next().css("color", "red");
            isValid = false;
        }
        else
        {
            $("#TBphone").next().text("");
            $("#TBphone").next().css("color", "#636363");
        }
        //Basic Address Validation
        if(address == ""){
            $("#TBaddress").next().text("* Address field can not be left empty.");
            $("#TBaddress").next().css("color", "red");
            isValid = false;
        }
        else
        {
            $("#TBaddress").next().text("");
            $("#TBaddress").next().css("color", "#636363");
        }

        if (!isValid) {
            evt.preventDefault();
        }
    });*/  
});