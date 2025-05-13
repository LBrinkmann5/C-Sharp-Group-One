window.addEventListener("beforeunload", function () {
    navigator.sendBeacon('/Employee/LogoutOnClose');
});