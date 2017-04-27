var RawSharer;
(function (RawSharer) {
    var PlayBack;
    (function (PlayBack) {
        var UI;
        (function (UI) {
            var Controls;
            (function (Controls) {
                var PlayState;
                (function (PlayState) {
                    var PlayerState = RawSharer.Enums.PlayerState;
                    var Indicator = (function () {
                        function Indicator(playerIconId, coverImgId) {
                            this.playerIconFront = $("#" + playerIconId);
                            this.coverImgFront = $("#" + coverImgId);
                        }
                        Indicator.prototype.playing = function () {
                            var _this = this;
                            this.coverImgFront.removeClass("animation-paused");
                            this.coverImgFront.addClass("animation-running");
                            this.playerIconFront.removeClass("fa-stop fa-pause animation-running state-switching state-switched");
                            this.playerIconFront.addClass("fa-play animation-running state-switching");
                            setTimeout(function () {
                                _this.playerIconFront.removeClass("animation-running state-switching");
                            }, 400);
                        };
                        Indicator.prototype.paused = function (ended) {
                            var _this = this;
                            this.playerIconFront.removeClass("fa-play animation-running state-switching");
                            if (ended)
                                this.stopped();
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
                        Indicator.prototype.stopped = function () {
                            var _this = this;
                            this.playerIconFront.addClass("fa-stop animation-running state-switching");
                            this.coverImgFront.removeClass("animation-running");
                            this.coverImgFront.addClass("animation-paused");
                            setTimeout(function () {
                                _this.coverImgFront.removeClass("animation-running state-switching");
                            }, 400);
                        };
                        Indicator.prototype.seeking = function (currentState) {
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
                        return Indicator;
                    }());
                    PlayState.Indicator = Indicator;
                })(PlayState = Controls.PlayState || (Controls.PlayState = {}));
            })(Controls = UI.Controls || (UI.Controls = {}));
        })(UI = PlayBack.UI || (PlayBack.UI = {}));
    })(PlayBack = RawSharer.PlayBack || (RawSharer.PlayBack = {}));
})(RawSharer || (RawSharer = {}));
//# sourceMappingURL=Indicator.js.map