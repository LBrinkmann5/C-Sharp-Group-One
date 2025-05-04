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

    
});