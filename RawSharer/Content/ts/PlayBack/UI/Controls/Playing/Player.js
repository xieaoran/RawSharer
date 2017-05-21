var RawSharer;
(function (RawSharer) {
    var PlayBack;
    (function (PlayBack) {
        var UI;
        (function (UI) {
            var Controls;
            (function (Controls) {
                var Playing;
                (function (Playing) {
                    var Player = (function () {
                        function Player(playerId) {
                            this.playerFront = document.getElementById(playerId);
                        }
                        Player.prototype.switchState = function () {
                            if (this.playerFront.paused)
                                this.playerFront.play();
                            else
                                this.playerFront.pause();
                        };
                        Player.prototype.seek = function (seekTime) {
                            var minDistance = this.playerFront.duration;
                            var minTime = 0;
                            for (var index = 0; index < this.playerFront.seekable.length; index++) {
                                var seekStart = this.playerFront.seekable.start(index);
                                var seekEnd = this.playerFront.seekable.end(index);
                                if (seekTime > seekStart && seekTime < seekEnd) {
                                    this.playerFront.currentTime = seekTime;
                                    return;
                                }
                                var seekStartDistance = Math.abs(seekTime - seekStart);
                                var seekEndDistance = Math.abs(seekTime - seekEnd);
                                if (seekStartDistance < minDistance) {
                                    minTime = seekStart;
                                    minDistance = seekStartDistance;
                                }
                                if (seekEndDistance < minDistance) {
                                    minTime = seekEnd;
                                    minDistance = seekEndDistance;
                                }
                            }
                            this.playerFront.currentTime = minTime;
                        };
                        Player.prototype.attachPlaying = function (playing) {
                            this.playerFront.onplaying = playing;
                        };
                        Player.prototype.attachPause = function (pause) {
                            var _this = this;
                            this.playerFront.onpause = function () { return pause(_this.playerFront.ended); };
                        };
                        Player.prototype.attachSeeking = function (seeking) {
                            this.playerFront.onseeking = seeking;
                        };
                        Player.prototype.attachTimeUpdate = function (timeUpdate) {
                            var _this = this;
                            this.playerFront.ontimeupdate = function () { return timeUpdate(_this.playerFront.currentTime); };
                        };
                        return Player;
                    }());
                    Playing.Player = Player;
                })(Playing = Controls.Playing || (Controls.Playing = {}));
            })(Controls = UI.Controls || (UI.Controls = {}));
        })(UI = PlayBack.UI || (PlayBack.UI = {}));
    })(PlayBack = RawSharer.PlayBack || (RawSharer.PlayBack = {}));
})(RawSharer || (RawSharer = {}));
//# sourceMappingURL=Player.js.map