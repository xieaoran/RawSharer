WinJS.Namespace.define("PlayBack", {
    Load: function () {
        PlayBack.UI.PlayerIcon = $("#player-icon");
        PlayBack.UI.CoverImg = $("#cover-img");
        PlayBack.Player.Main = $("#player")[0];
        PlayBack.Lyric.LyricContainer = $("#lyricContainer");
        PlayBack.Lyric.LyricUrl = PlayBack.Lyric.LyricContainer.data("lyrics");
        PlayBack.Lyric.LoadLyric();
        PlayBack.UI.IconStop();
        PlayBack.Player.Main.onplaying = PlayBack.UI.IconPlaying;
        PlayBack.Player.Main.onpause = PlayBack.UI.IconPause;
        PlayBack.Player.Main.onseeking = PlayBack.UI.IconSeeking;
    }
});

WinJS.Namespace.defineWithParent(PlayBack, "Enums", {
    Status: { Playing: 0, Stopped: 1, Paused: 2, Seeking: 3 }
});

WinJS.Namespace.defineWithParent(PlayBack, "UI", {
    TimeOut: undefined,
    IconPlaying: function() {
        if (PlayBack.Player.CurrentStatus === PlayBack.Enums.Status.Seeking) {
            PlayBack.Player.CurrentStatus = PlayBack.Enums.Status.Playing;
            return;
        }
        PlayBack.Player.CurrentStatus = PlayBack.Enums.Status.Playing;
        PlayBack.UI.CoverImg.removeClass("animation-paused");
        PlayBack.UI.CoverImg.addClass("animation-running");
        PlayBack.UI.PlayerIcon.removeClass("fa-stop fa-pause animation-running state-switching state-switched");
        PlayBack.UI.PlayerIcon.addClass("fa-play animation-running state-switching");
        setTimeout(function () {
            PlayBack.UI.PlayerIcon.removeClass("animation-running state-switching");
        }, 400);
    },
    IconPause: function() {
        PlayBack.Player.CurrentStatus = PlayBack.Enums.Status.Paused;
        PlayBack.UI.PlayerIcon.removeClass("fa-play animation-running state-switching");
        if (PlayBack.Player.Main.ended) {
            PlayBack.UI.IconStop();
        }
        else {
            PlayBack.UI.PlayerIcon.addClass("fa-pause animation-running state-switching");
            PlayBack.UI.CoverImg.removeClass("animation-running");
            PlayBack.UI.CoverImg.addClass("animation-paused");
            PlayBack.UI.TimeOut = setTimeout(function () {
                PlayBack.UI.PlayerIcon.removeClass("animation-running state-switching");
                PlayBack.UI.PlayerIcon.addClass("animation-running state-switched");
            }, 400);
        }
    },
    IconStop: function() {
        PlayBack.Player.CurrentStatus = PlayBack.Enums.Status.Stopped;
        PlayBack.UI.PlayerIcon.addClass("fa-stop animation-running state-switching");
        PlayBack.UI.CoverImg.removeClass("animation-running");
        PlayBack.UI.CoverImg.addClass("animation-paused");
        setTimeout(function () {
            PlayBack.UI.PlayerIcon.removeClass("animation-running state-switching");
        }, 400);
    },
    IconSeeking: function() {
        if (PlayBack.UI.TimeOut) {
            clearTimeout(PlayBack.UI.TimeOut);
        }
        if (PlayBack.Player.CurrentStatus === PlayBack.Enums.Status.Playing) {
            PlayBack.Player.CurrentStatus = PlayBack.Enums.Status.Seeking;
            PlayBack.UI.PlayerIcon.removeClass("fa-pause animation-running state-switching state-switched");
            PlayBack.UI.PlayerIcon.addClass("fa-play");
            PlayBack.UI.CoverImg.removeClass("animation-paused");
            PlayBack.UI.CoverImg.addClass("animation-running");
        }
        else if (PlayBack.Player.CurrentStatus === PlayBack.Enums.Status.Stopped) {
            PlayBack.Player.CurrentStatus = PlayBack.Enums.Status.Paused;
            PlayBack.UI.PlayerIcon.removeClass("fa-stop animation-running state-switching");
            PlayBack.UI.PlayerIcon.addClass("fa-pause animation-running state-switched");
        }
    }
});

WinJS.Namespace.defineWithParent(PlayBack, "Player", {
    Main: undefined,
    CurrentStatus: PlayBack.Enums.Status.Stopped,
    SwitchState: function () {
        if (PlayBack.Player.Main.paused) {
            PlayBack.Player.Main.play();
        }
        else {
            PlayBack.Player.Main.pause();
        }
    },
    MouseSeek: function(e) {
        var scope, seekIndex;
        if (Helpers.Browser.GetBrowser() === "Firefox") {
            scope = e.detail / 3;
            seekIndex = PlayBack.Lyric.CurrentIndex + scope;
        }
        else {
            scope = e.wheelDelta / 120;
            seekIndex = PlayBack.Lyric.CurrentIndex - scope;
        }
        PlayBack.Player.Seek(seekIndex);
        return false;
    },
    KeyboardSeek: function(e) {
        var keyCode = Helpers.Browser.GetBrowser() === "MSIE" ? e.keyCode : e.which;
        if (keyCode === 38) {
            PlayBack.Player.Seek(PlayBack.Lyric.CurrentIndex - 1);
        }
        else if (keyCode === 40) {
            PlayBack.Player.Seek(PlayBack.Lyric.CurrentIndex + 1);
        }
        return false;
    },
    Seek: function(seekIndex) {
        if (seekIndex < 0) {
            seekIndex = 0;
        }
        var seekTime = PlayBack.Lyric.Main[seekIndex][0];
        var minDistance = PlayBack.Player.Main.duration;
        var seekStart, seekEnd, seekStartDistance, seekEndDistance;
        var minTime = 0;
        for (var index = 0; index < PlayBack.Player.Main.seekable.length; index++) {
            seekStart = PlayBack.Player.Main.seekable.start(index);
            seekEnd = PlayBack.Player.Main.seekable.end(index);
            if (seekTime > seekStart && seekTime < seekEnd) {
                PlayBack.Player.Main.currentTime = seekTime;
                return;
            }
            seekStartDistance = Math.abs(seekTime - seekStart);
            seekEndDistance = Math.abs(seekTime - seekEnd);
            if (seekStartDistance < minDistance) {
                minTime = seekStart;
                minDistance = seekStartDistance;
            }
            if (seekEndDistance < minDistance) {
                minTime = seekEnd;
                minDistance = seekEndDistance;
            }
        }
        PlayBack.Player.Main.currentTime = minTime;
    }
});

WinJS.Namespace.defineWithParent(PlayBack, "Lyric", {
    Main: [],
    LyricContainer: undefined,
    LyricUrl: undefined,
    Pattern: /\[\d{2}:\d{2}.\d{2}\]/g,
    LastIndex: -1,
    CurrentIndex: 0,
    LoadLyric: function() {
        var request = new XMLHttpRequest();
        request.open("GET", PlayBack.Lyric.LyricUrl, true);
        request.responseType = "text";
        request.onload = function () {
            PlayBack.Lyric.Main = PlayBack.Lyric.ParseLyric(request.response);
            for (var index = 0; index < PlayBack.Lyric.Main.length; index++) {
                var lyricSentence = document.createElement("p");
                if (Helpers.Browser.GetBrowser() === "Firefox") {
                    var currentSentence = PlayBack.Lyric.Main[index][1];
                    if (currentSentence === false) {
                        lyricSentence.innerHTML = "<br/>";
                    }
                    else {
                        lyricSentence.innerHTML = PlayBack.Lyric.Main[index][1];
                    }
                }
                else {
                    lyricSentence.innerText = PlayBack.Lyric.Main[index][1];
                }
                lyricSentence.id = "lyric" + index;
                PlayBack.Lyric.LyricContainer.append(lyricSentence);
            }
            PlayBack.Player.Main.ontimeupdate = function () {
                for (var index = 0; index < PlayBack.Lyric.Main.length; index++) {
                    if (this.currentTime < PlayBack.Lyric.Main[index][0]) {
                        PlayBack.Lyric.CurrentIndex = index - 1;
                        break;
                    }
                }
                if (PlayBack.Lyric.CurrentIndex === PlayBack.Lyric.LastIndex) return;
                var lastLyricSentence = $("#lyric" + PlayBack.Lyric.LastIndex);
                var currentLyricSentence = $("#lyric" + PlayBack.Lyric.CurrentIndex);
                lastLyricSentence.stop();
                lastLyricSentence.css("font-weight", "normal");
                lastLyricSentence.animate({ "color": "white" }, "fast");
                currentLyricSentence.stop();
                currentLyricSentence.css("font-weight", "bold");
                currentLyricSentence.animate({ "color": "#FFC107" }, "fast");
                PlayBack.Lyric.LyricContainer.stop();
                PlayBack.Lyric.LyricContainer.animate({ "top": 140 - PlayBack.Lyric.CurrentIndex * 35 }, "fast", "easeOutCubic");
                PlayBack.Lyric.LastIndex = PlayBack.Lyric.CurrentIndex;
            };
            if (Helpers.Browser.GetBrowser() === "Firefox") {
                document.addEventListener("DOMMouseScroll", PlayBack.Player.MouseSeek, false);
            }
            else {
                document.onmousewheel = PlayBack.Player.MouseSeek;
            }
            document.onkeydown = PlayBack.Player.KeyboardSeek;
        };
        request.send();
    },
    ParseLyric: function(text) {
        var lines = text.split("\n"), result = [];
        while (!PlayBack.Lyric.Pattern.test(lines[0])) {
            lines = lines.slice(1);
        }
        lines[lines.length - 1].length === 0 && lines.pop();
        lines.forEach(function (line) {
            var time = line.match(PlayBack.Lyric.Pattern),
                value = line.replace(PlayBack.Lyric.Pattern, "");
            time.forEach(function (line) {
                var t = line.slice(1, -1).split(":");
                result.push([parseInt(t[0], 10) * 60 + parseFloat(t[1]), value]);
            });
        });
        result.sort(function (a, b) {
            return a[0] - b[0];
        });
        return result;
    }
});