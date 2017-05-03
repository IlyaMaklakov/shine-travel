var Shine = function () {
    var player;
    var currentVideoIndex = 0;
    var videoInfoUrl = "https://vimeo.com/api/oembed.json?url=https%3A//vimeo.com/";
    var handleInit = function () {
        getVideosInfo();
        var options = {
            id: videoItems[currentVideoIndex].vimeoId,
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

        setActiveLink(videoItems[0].vimeoId);
    };

    function setActiveLink(videoId) {
        $('#recent-playlist li').removeClass('active');
        $("#" + videoId).parent('li').addClass("active");
    };


    function getVideosInfo() {
        var videoInfoList = "";

        $.ajaxSetup({
            async: false
        });

        for (var i = 0; i < videoItems.length; i++) {
            $.getJSON(videoInfoUrl + videoItems[i].vimeoId, function (data) {             
                videoInfoList +=
                    "<li>"
                + '<a href="#" class="video-link" id="' + data.video_id +'" title= "" >'
                        +'<div class="row">'
                            +'<div class="col-md-5">'
                                +'<div class="c-image">'
                                     + '<img src="' + data.thumbnail_url + '" class="img-responsive" alt="">'
                                    + "</div>"
                                + "</div>"
                                + '<div class="col-md-7">'
                                    + '<div class="c-post">'
                                        + '<h5 class="c-title">' + data.title + "</h5>"
                                        + '<div class="c-short-text" > '
                                            + "<p>" + data.author_name + "</p>"
                                        + "</div> "
                                    + "</div > "
                                + "</div > "
                            + "</div > "
                        + "</a > "
                    + "</li >";
            });
        }

        $.ajaxSetup({
            async: true
        });

        $("#recent-playlist").html(videoInfoList);

        $(".video-link").click(function (e) {
            var id = $(this).attr("id");
            setActiveLink(id);
            player.loadVideo(id);
        });
    }

    return {
        init: function () {
            handleInit();
        }
    };
}();