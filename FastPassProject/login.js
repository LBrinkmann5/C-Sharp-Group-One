$(document).ready( () =>
{
    const Testusername = "MyUser";
    const Testpass = "This is my test password12345$";
    $(document).on("submit", "#log_in_form", function(evt)
    {   
        const usernameField = document.querySelector(".popover #TBuser");
        const passwordField = document.querySelector(".popover #TBpass");
        const username = usernameField ? usernameField.value.trim() : "";
        const password = passwordField ? passwordField.value.trim() : "";
        
        let isValid = false;
        if(username == Testusername && password == Testpass)
        {
            isValid = true;
        }


        if(isValid)
        {
            window.alert(`User ${username} has been logged in.`);
        }
        else if(!isValid)
        {
            evt.preventDefault();
            window.alert(`Incorrect log in information.`);
        }
    });

    //Show and Hide Password
    $(document).on("click", ".popover #btnShow", function(){
        const passwordField = document.querySelector(".popover #TBpass");
        const buttonIcon = document.querySelector(".popover #btnShow");
        const fieldType = passwordField.type;

        if (fieldType === "password"){
            passwordField.type="text";
            buttonIcon.className = "btn btnShow bi-eye-slash-fill";
        }
        else {
            passwordField.type = "password";
            buttonIcon.className = "btn btnShow bi-eye-fill";
        }


    });
});