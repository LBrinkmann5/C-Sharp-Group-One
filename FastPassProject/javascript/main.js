"use strict";
document.addEventListener('DOMContentLoaded', function() {
    //Initializes popovers
    const popoverTriggerList = document.querySelectorAll('[data-bs-toggle="popover"]');
    const popoverList = [...popoverTriggerList].map(popoverTriggerEl => new bootstrap.Popover(popoverTriggerEl));
    const popoverTriggerEl = document.getElementById('loginStart');
    const popoverContent = document.getElementById('popoverContent').innerHTML;
//Allows popover-container to appear
    const popover = new bootstrap.Popover(popoverTriggerEl, {
        content: popoverContent,
        html: true,
        trigger: 'manual',
        sanitize:false,
        customClass: 'custom-popover'
    });
    $(popoverTriggerEl).on("shown.bs.popover", function() {
        console.log("Popover is now visible");
    });
    

//Hide popover only when outside clicked
    document.addEventListener('click', function(event) 
    {
    const isClickInside = popoverTriggerEl.contains(event.target) || document.querySelector('.popover')?.contains(event.target);
    if (!isClickInside) 
        {
            popover.hide();
        }
    });

    
    
    
    
});
