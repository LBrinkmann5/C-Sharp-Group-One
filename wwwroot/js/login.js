$(document).ready( () =>
{
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

    $(document).on("change", ".popover #CBemployee", function () {
        //const form = document.getElementById("log_in_form");
        const form = document.querySelector(".popover #log_in_form");
        if (this.checked) {
            form.action = "/Employee/Login";
            console.log("Form action updated to: ", form.action);
        }
        else
        {
            form.action = "/Home/Login"
            console.log("Form action updated to: ", form.action);
        }
    });
    
});