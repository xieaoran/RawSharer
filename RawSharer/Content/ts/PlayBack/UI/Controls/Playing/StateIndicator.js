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
                    var PlayerState = PlayBack.Utils.PlayerState;
                    class StateIndicator {
                        constructor(playerIconId, coverImgId) {
                            this.playerIconFront = $(`#${playerIconId}`);
                            this.coverImgFront = $(`#${coverImgId}`);
                        }
                        toPlaying() {
                            this.coverImgFront.removeClass("animation-paused");
                            this.coverImgFront.addClass("animation-running");
                            this.playerIconFront.removeClass("fa-stop fa-pause animation-running state-switching state-switched");
                            this.playerIconFront.addClass("fa-play animation-running state-switching");
                            setTimeout(() => {
                                this.playerIconFront.removeClass("animation-running state-switching");
                            }, 400);
                        }
                        toPaused(ended) {
                            this.playerIconFront.removeClass("fa-play animation-running state-switching");
                            if (ended)
                                this.toStopped();
                            else {
                                this.playerIconFront.addClass("fa-pause animation-running state-switching");
                                this.coverImgFront.removeClass("animation-running");
                                this.coverImgFront.addClass("animation-paused");
                                this.pauseTimeOut = setTimeout(() => {
                                    this.playerIconFront.removeClass("animation-running state-switching");
                                    this.playerIconFront.addClass("animation-running state-switched");
                                }, 400);
                            }
                        }
                        toStopped() {
                            this.playerIconFront.addClass("fa-stop animation-running state-switching");
                            this.coverImgFront.removeClass("animation-running");
                            this.coverImgFront.addClass("animation-paused");
                            setTimeout(() => {
                                this.coverImgFront.removeClass("animation-running state-switching");
                            }, 400);
                        }
                        toSeeking(currentState) {
                            if (this.pauseTimeOut)
                                clearTimeout(this.pauseTimeOut);
                            if (currentState === PlayerState.Playing) {
                                this.playerIconFront.removeClass("fa-pause animation-running state-switching state-switched");
                                this.playerIconFront.addClass("fa-play");
                                this.coverImgFront.removeClass("animation-paused");
                                this.coverImgFront.addClass("animation-running");
                            }
                            else if (currentState === PlayerState.Stopped) {
                                this.playerIconFront.removeClass("fa-stop animation-running state-switching");
                                this.playerIconFront.addClass("fa-pause animation-running state-switched");
                            }
                        }
                        attachClick(click) {
                            this.playerIconFront.click(click);
                        }
                    }
                    Playing.StateIndicator = StateIndicator;
                })(Playing = Controls.Playing || (Controls.Playing = {}));
            })(Controls = UI.Controls || (UI.Controls = {}));
        })(UI = PlayBack.UI || (PlayBack.UI = {}));
    })(PlayBack = RawSharer.PlayBack || (RawSharer.PlayBack = {}));
})(RawSharer || (RawSharer = {}));
//# sourceMappingURL=StateIndicator.js.map