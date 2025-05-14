window.addEventListener('beforeunload', function () {
    sessionStorage.setItem('navigating', 'true');
});

document.addEventListener('click', function (e) {
    if (e.target.tagName === 'A' && e.target.href) {
        sessionStorage.setItem('navigating', 'true');
    }
});

document.addEventListener('submit', function () {
    sessionStorage.setItem('navigating', 'true');
});


window.addEventListener('unload', function () {
    if (!sessionStorage.getItem('navigating')) {
        // Call your logout endpoint here
        navigator.sendBeacon('/Employee/Logout');
    }
    sessionStorage.removeItem('navigating');
});

window.addEventListener('load', function () {
    sessionStorage.removeItem('navigating');
});