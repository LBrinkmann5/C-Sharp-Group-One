"use strict";
$(document).ready(() => 
{
    const passwordvalid = /^(?=.*\d)(?=.*[A-Z])(?=.*[!@#$%^&*()-_=+,.?":])(?!.*\s\s).{10,}$/
    const userList = [];

    $("#customer_register").submit( evt =>
    {
        const username = $("#TBuser").val().trim();
        const password = $("#TBpass").val().trim();
        let isValid = true;

        //Basic username validation
        if(username == "")
        {
            window.alert("Username field can not be left empty.");
            isValid = false;
        }
        //Basic password validation
        if(password == ""){
            window.alert("Password field can not be left empty.")
            isValid = false;
        }
        else if(!passwordvalid.test(password))
        {
            window.alert("Password must be 12 characters long, contain a capital letter, a number, and a special character. Phrase passwords must have no more than one space between words.")
            isValid = false;
        }
        if(!isValid){
            evt.preventDefault();
        }
        else if(isValid){
            window.alert(`Username: ${username}, Password: ${password}`);          
        }
    });    
});