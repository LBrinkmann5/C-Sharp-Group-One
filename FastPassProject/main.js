document.addEventListener('DOMContentLoaded', function() {
    const popoverTriggerList = document.querySelectorAll('[data-bs-toggle="popover"]');
    const popoverList = [...popoverTriggerList].map(popoverTriggerEl => new bootstrap.Popover(popoverTriggerEl));
    const popoverTriggerEl = document.getElementById('loginStart');
    const popoverContent = document.getElementById('popoverContent').innerHTML;

    const popover = new bootstrap.Popover(popoverTriggerEl, {
        content: popoverContent,
        html: true,
        trigger: 'manual',
        sanitize:false,
        customClass: 'custom-popover'
    });

    popoverTriggerEl.addEventListener('click', function() {
        popover.show();
    });

    document.addEventListener('click', function(event) {
    const isClickInside = popoverTriggerEl.contains(event.target) || document.querySelector('.popover')?.contains(event.target);
    if (!isClickInside) {
        popover.hide();
    }

    
    });
});
