var RawSharer;
(function (RawSharer) {
    var PlayBack;
    (function (PlayBack) {
        var Core;
        (function (Core) {
            var Utils = RawSharer.Common.Utils;
            var BrowserType = RawSharer.Common.Utils.BrowserType;
            var PlayerState = PlayBack.Utils.PlayerState;
            var PlayBackController = (function () {
                function PlayBackController(player, stateIndicator, container) {
                    var _this = this;
                    this.playerPlayingHandler = function () {
                        if (_this.currentState === PlayerState.Seeking) {
                            _this.currentState = PlayerState.Playing;
                            return;
                        }
                        _this.currentState = PlayerState.Playing;
                        _this.stateIndicator.toPlaying();
                    };
                    this.playerPauseHandler = function (ended) {
                        _this.currentState = ended ? PlayerState.Stopped : PlayerState.Paused;
                        _this.stateIndicator.toPaused(ended);
                    };
                    this.playerSeekingHandler = function () {
                        var previousState = _this.currentState;
                        if (_this.currentState === PlayerState.Playing)
                            _this.currentState = PlayerState.Seeking;
                        else if (_this.currentState === PlayerState.Stopped)
                            _this.currentState = PlayerState.Paused;
                        _this.stateIndicator.toSeeking(previousState);
                    };
                    this.playerTimeUpdateHandler = function (currentTime) {
                        _this.currentSentenceIndex = _this.getSentenceIndex(currentTime);
                        if (_this.currentSentenceIndex === _this.lastSentenceIndex)
                            return;
                        _this.container.readLine(_this.lastSentenceIndex, _this.currentSentenceIndex);
                        _this.lastSentenceIndex = _this.currentSentenceIndex;
                    };
                    this.stateIndicatorClickHandler = function () {
                        _this.player.switchState();
                    };
                    this.containerMouseWheelHandler = function (e) {
                        var sentenceIndex;
                        if (Utils.Environment.browser === BrowserType.Firefox) {
                            var scope = e.detail / 3;
                            sentenceIndex = _this.currentSentenceIndex + scope;
                        }
                        else {
                            var scope = e.wheelDelta / 120;
                            sentenceIndex = _this.currentSentenceIndex - scope;
                        }
                        _this.seek(sentenceIndex);
                        return false;
                    };
                    this.containerKeyDownHandler = function (e) {
                        var keyCode = Utils.Environment.browser === BrowserType.MSIE ? e.keyCode : e.which;
                        if (keyCode === 38) {
                            _this.seek(_this.currentSentenceIndex - 1);
                        }
                        else if (keyCode === 40) {
                            _this.seek(_this.currentSentenceIndex + 1);
                        }
                        return false;
                    };
                    this.player = player;
                    this.stateIndicator = stateIndicator;
                    this.container = container;
                    this.currentState = PlayerState.Stopped;
                    this.lastSentenceIndex = -1;
                    this.currentSentenceIndex = 0;
                    this.sentenceTimes = this.container.getSentenceTimes();
                    this.player.attachPlaying(this.playerPlayingHandler);
                    this.player.attachPause(this.playerPauseHandler);
                    this.player.attachSeeking(this.playerSeekingHandler);
                    this.player.attachTimeUpdate(this.playerTimeUpdateHandler);
                    this.stateIndicator.attachClick(this.stateIndicatorClickHandler);
                    this.container.attachMouseWheel(this.containerMouseWheelHandler);
                    this.container.attachKeyDown(this.containerKeyDownHandler);
                    this.stateIndicator.toStopped();
                }
                PlayBackController.prototype.seek = function (sentenceIndex) {
                    if (sentenceIndex < 0)
                        sentenceIndex = 0;
                    else if (sentenceIndex > this.sentenceTimes.length - 1)
                        sentenceIndex = this.sentenceTimes.length - 1;
                    var seekTime = this.sentenceTimes[sentenceIndex];
                    this.player.seek(seekTime);
                };
                PlayBackController.prototype.getSentenceIndex = function (currentTime) {
                    if (currentTime === this.sentenceTimes[this.currentSentenceIndex])
                        return this.currentSentenceIndex;
                    else if (currentTime > this.sentenceTimes[this.currentSentenceIndex]) {
                        for (var index = this.currentSentenceIndex + 1; index < this.sentenceTimes.length; index++) {
                            if (currentTime < this.sentenceTimes[index])
                                return index - 1;
                        }
                        return this.sentenceTimes.length - 1;
                    }
                    else {
                        for (var index = this.currentSentenceIndex - 1; index >= 0; index--) {
                            if (currentTime > this.sentenceTimes[index])
                                return index;
                        }
                        return 0;
                    }
                };
                return PlayBackController;
            }());
            Core.PlayBackController = PlayBackController;
        })(Core = PlayBack.Core || (PlayBack.Core = {}));
    })(PlayBack = RawSharer.PlayBack || (RawSharer.PlayBack = {}));
})(RawSharer || (RawSharer = {}));
//# sourceMappingURL=PlayBackController.js.map