var Shine = function () {
    var player;
    var handleInit = function (videoVimeoId) {
        var options = {
            url: videoVimeoId,
            width: 640,
            loop: false,
            portrait: false,
            byline: false,
            title: false
        };

        player = new window.Vimeo.Player("videoContainer", options);
        player.setVolume(0.5);
       
        player.on('loaded', function (data) {
            player.play();
            player.disableTextTrack();
        });
    };
    return {
        init: function (videoVimeoId) {
            handleInit(videoVimeoId);
        }
    };
}();