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
                    var StateIndicator = (function () {
                        function StateIndicator(playerIconId, coverImgId) {
                            this.playerIconFront = $("#" + playerIconId);
                            this.coverImgFront = $("#" + coverImgId);
                        }
                        StateIndicator.prototype.toPlaying = function () {
                            var _this = this;
                            this.coverImgFront.removeClass("animation-paused");
                            this.coverImgFront.addClass("animation-running");
                            this.playerIconFront.removeClass("fa-stop fa-pause animation-running state-switching state-switched");
                            this.playerIconFront.addClass("fa-play animation-running state-switching");
                            setTimeout(function () {
                                _this.playerIconFront.removeClass("animation-running state-switching");
                            }, 400);
                        };
                        StateIndicator.prototype.toPaused = function (ended) {
                            var _this = this;
                            this.playerIconFront.removeClass("fa-play animation-running state-switching");
                            if (ended)
                                this.toStopped();
                            else {
                                this.playerIconFront.addClass("fa-pause animation-running state-switching");
                                this.coverImgFront.removeClass("animation-running");
                                this.coverImgFront.addClass("animation-paused");
                                this.pauseTimeOut = setTimeout(function () {
                                    _this.playerIconFront.removeClass("animation-running state-switching");
                                    _this.playerIconFront.addClass("animation-running state-switched");
                                }, 400);
                            }
                        };
                        StateIndicator.prototype.toStopped = function () {
                            var _this = this;
                            this.playerIconFront.addClass("fa-stop animation-running state-switching");
                            this.coverImgFront.removeClass("animation-running");
                            this.coverImgFront.addClass("animation-paused");
                            setTimeout(function () {
                                _this.coverImgFront.removeClass("animation-running state-switching");
                            }, 400);
                        };
                        StateIndicator.prototype.toSeeking = function (currentState) {
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
                        };
                        StateIndicator.prototype.attachClick = function (click) {
                            this.playerIconFront.click(click);
                        };
                        return StateIndicator;
                    }());
                    Playing.StateIndicator = StateIndicator;
                })(Playing = Controls.Playing || (Controls.Playing = {}));
            })(Controls = UI.Controls || (UI.Controls = {}));
        })(UI = PlayBack.UI || (PlayBack.UI = {}));
    })(PlayBack = RawSharer.PlayBack || (RawSharer.PlayBack = {}));
})(RawSharer || (RawSharer = {}));
//# sourceMappingURL=StateIndicator.js.map