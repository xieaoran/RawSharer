var RawSharer;
(function (RawSharer) {
    var PlayBack;
    (function (PlayBack) {
        var Core;
        (function (Core) {
            var Utils = RawSharer.Common.Utils;
            var BrowserType = RawSharer.Common.Utils.BrowserType;
            var PlayerState = PlayBack.Utils.PlayerState;
            class PlayBackController {
                constructor(player, stateIndicator, container) {
                    this.playerPlayingHandler = () => {
                        if (this.currentState === PlayerState.Seeking) {
                            this.currentState = PlayerState.Playing;
                            return;
                        }
                        this.currentState = PlayerState.Playing;
                        this.stateIndicator.toPlaying();
                    };
                    this.playerPauseHandler = (ended) => {
                        this.currentState = ended ? PlayerState.Stopped : PlayerState.Paused;
                        this.stateIndicator.toPaused(ended);
                    };
                    this.playerSeekingHandler = () => {
                        const previousState = this.currentState;
                        if (this.currentState === PlayerState.Playing)
                            this.currentState = PlayerState.Seeking;
                        else if (this.currentState === PlayerState.Stopped)
                            this.currentState = PlayerState.Paused;
                        this.stateIndicator.toSeeking(previousState);
                    };
                    this.playerTimeUpdateHandler = (currentTime) => {
                        this.currentSentenceIndex = this.getSentenceIndex(currentTime);
                        if (this.currentSentenceIndex === this.lastSentenceIndex)
                            return;
                        this.container.readLine(this.lastSentenceIndex, this.currentSentenceIndex);
                        this.lastSentenceIndex = this.currentSentenceIndex;
                    };
                    this.stateIndicatorClickHandler = () => {
                        this.player.switchState();
                    };
                    this.containerMouseWheelHandler = (e) => {
                        let sentenceIndex;
                        if (Utils.Environment.browser === BrowserType.Firefox) {
                            const scope = e.detail / 3;
                            sentenceIndex = this.currentSentenceIndex + scope;
                        }
                        else {
                            const scope = e.wheelDelta / 120;
                            sentenceIndex = this.currentSentenceIndex - scope;
                        }
                        this.seek(sentenceIndex);
                        return false;
                    };
                    this.containerKeyDownHandler = (e) => {
                        const keyCode = Utils.Environment.browser === BrowserType.MSIE ? e.keyCode : e.which;
                        if (keyCode === 38) {
                            this.seek(this.currentSentenceIndex - 1);
                        }
                        else if (keyCode === 40) {
                            this.seek(this.currentSentenceIndex + 1);
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
                seek(sentenceIndex) {
                    if (sentenceIndex < 0)
                        sentenceIndex = 0;
                    else if (sentenceIndex > this.sentenceTimes.length - 1)
                        sentenceIndex = this.sentenceTimes.length - 1;
                    const seekTime = this.sentenceTimes[sentenceIndex];
                    this.player.seek(seekTime);
                }
                getSentenceIndex(currentTime) {
                    if (currentTime === this.sentenceTimes[this.currentSentenceIndex])
                        return this.currentSentenceIndex;
                    else if (currentTime > this.sentenceTimes[this.currentSentenceIndex]) {
                        for (let index = this.currentSentenceIndex + 1; index < this.sentenceTimes.length; index++) {
                            if (currentTime < this.sentenceTimes[index])
                                return index - 1;
                        }
                        return this.sentenceTimes.length - 1;
                    }
                    else {
                        for (let index = this.currentSentenceIndex - 1; index >= 0; index--) {
                            if (currentTime > this.sentenceTimes[index])
                                return index;
                        }
                        return 0;
                    }
                }
            }
            Core.PlayBackController = PlayBackController;
        })(Core = PlayBack.Core || (PlayBack.Core = {}));
    })(PlayBack = RawSharer.PlayBack || (RawSharer.PlayBack = {}));
})(RawSharer || (RawSharer = {}));
//# sourceMappingURL=PlayBackController.js.map