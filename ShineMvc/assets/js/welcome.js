var Welcome = function () {
    var handleInit = function (redirectUrl) {
        window.location.replace(redirectUrl);
    };
    return {
        init: function (redirectUrl) {
            AmoCrm.auth("maa@shine.city", "b526e7df7b1f22b34a3b8a1d3b7530f0");
            //handleInit(redirectUrl);
        }
    };
}();