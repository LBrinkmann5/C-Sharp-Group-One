"use strict";
document.addEventListener('DOMContentLoaded', function() {
    //Initializes popovers
    //const popoverTriggerList = document.querySelectorAll('[data-bs-toggle="popover"]');
    //const popoverList = [...popoverTriggerList].map(popoverTriggerEl => new bootstrap.Popover(popoverTriggerEl));
    /*const popoverTriggerEl = document.getElementById('loginStart');*/
    const popoverContent = document.getElementById('popoverContent').innerHTML;

//Allows popover-container to appear
    //const popover = new bootstrap.Popover(popoverTriggerEl, {
    //    content: popoverContent,
    //    html: true,
    //    trigger: 'manual',
    //    sanitize:false,
    //    customClass: 'custom-popover'
    //});
    const popoverButtons = document.querySelectorAll('.loginStart');
    popoverButtons.forEach(button => {
        const popover = new bootstrap.Popover(button,
            {
                content: popoverContent,
                html: true,
                trigger: 'manual',
                sanitize: false,
                customClass: 'custom-popover'
            });
        button.addEventListener('click', function (event) {
            event.preventDefault();
            popover.show();
        });

    });

    $(popoverButtons).on("shown.bs.popover", function ()
    {
        $('#ReturnUrl').val(window.location.href);
        console.log("Popover is now visible");
    });

    //$(popoverTriggerEl).on("shown.bs.popover", function () {
    //    $('#ReturnUrl').val(window.location.href);
    //    console.log("Popover is now visible");
    //});




    //Hide popover only when outside clicked
    document.addEventListener('click', function (event) {
        const isClickInside = Array.from(popoverButtons).some(button =>
            button.contains(event.target) || document.querySelector('.popover')?.contains(event.target)
        );
        if (!isClickInside)
        {
            popoverButtons.forEach(button =>
            {
                const popoverInstance = bootstrap.Popover.getInstance(button);
                if (popoverInstance) {
                    popoverInstance.hide();
                }
            });
        }
    });
    //document.addEventListener('click', function(event) 
    //{
    //const isClickInside = popoverTriggerEl.contains(event.target) || document.querySelector('.popover')?.contains(event.target);
    //if (!isClickInside) 
    //    {
    //        popover.hide();
    //    }
    //});

        // Attach event listener to buttons that require authentication
    document.querySelectorAll(".requires-auth").forEach(button => {
        button.addEventListener("click", function (event) {
            if (!isAuthenticated) {
                event.preventDefault(); // Prevent the default action
                const loginModal = new bootstrap.Modal(document.getElementById("loginModal"));
                loginModal.show();
            }
        });
    });

    //Login Error Modal
    //if (errorMessage != "")
    //{
    //    const errorModal = new bootstrap.Modal(document.getElementById("errorModal"));
    //    errorModal.show();
    //}
    
    
});
