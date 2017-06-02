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
                    class Player {
                        constructor(playerId) {
                            this.playerFront = document.getElementById(playerId);
                        }
                        switchState() {
                            if (this.playerFront.paused)
                                this.playerFront.play();
                            else
                                this.playerFront.pause();
                        }
                        seek(seekTime) {
                            let minDistance = this.playerFront.duration;
                            let minTime = 0;
                            for (let index = 0; index < this.playerFront.seekable.length; index++) {
                                const seekStart = this.playerFront.seekable.start(index);
                                const seekEnd = this.playerFront.seekable.end(index);
                                if (seekTime > seekStart && seekTime < seekEnd) {
                                    this.playerFront.currentTime = seekTime;
                                    return;
                                }
                                const seekStartDistance = Math.abs(seekTime - seekStart);
                                const seekEndDistance = Math.abs(seekTime - seekEnd);
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
                        }
                        attachPlaying(playing) {
                            this.playerFront.onplaying = playing;
                        }
                        attachPause(pause) {
                            this.playerFront.onpause = () => pause(this.playerFront.ended);
                        }
                        attachSeeking(seeking) {
                            this.playerFront.onseeking = seeking;
                        }
                        attachTimeUpdate(timeUpdate) {
                            this.playerFront.ontimeupdate = () => timeUpdate(this.playerFront.currentTime);
                        }
                    }
                    Playing.Player = Player;
                })(Playing = Controls.Playing || (Controls.Playing = {}));
            })(Controls = UI.Controls || (UI.Controls = {}));
        })(UI = PlayBack.UI || (PlayBack.UI = {}));
    })(PlayBack = RawSharer.PlayBack || (RawSharer.PlayBack = {}));
})(RawSharer || (RawSharer = {}));
//# sourceMappingURL=Player.js.map