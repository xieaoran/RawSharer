namespace RawSharer.PlayBack.Core {
    import Utils = Common.Utils;
    import BrowserType = Common.Utils.BrowserType;
    import PlayerState = PlayBack.Utils.PlayerState;

    import Player = PlayBack.UI.Controls.Playing.Player;
    import StateIndicator = PlayBack.UI.Controls.Playing.StateIndicator;
    import LyricsContainer = PlayBack.UI.Controls.Lyrics.LyricsContainer;

    export class PlayBackController {
        private readonly player: Player;
        private readonly stateIndicator: StateIndicator;
        private readonly container: LyricsContainer;

        private currentState: PlayerState;
        private lastSentenceIndex: number;
        private currentSentenceIndex: number;

        private readonly sentenceTimes: number[];

        public constructor(player: Player, stateIndicator: StateIndicator, container: LyricsContainer) {
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

        public seek(sentenceIndex: number): void {
            if (sentenceIndex < 0) sentenceIndex = 0;
            const seekTime = this.sentenceTimes[sentenceIndex];
            this.player.seek(seekTime);
        }

        private getSentenceIndex(currentTime: number): number {
            if (currentTime === this.sentenceTimes[this.currentSentenceIndex])
                return this.currentSentenceIndex;
            else if (currentTime > this.sentenceTimes[this.currentSentenceIndex]) {
                for (let index = this.currentSentenceIndex + 1; index < this.sentenceTimes.length; index++) {
                    if (currentTime < this.sentenceTimes[index]) return index - 1;
                }
                return this.sentenceTimes.length - 1;
            }
            else if (currentTime < this.sentenceTimes[this.currentSentenceIndex]) {
                for (let index = this.currentSentenceIndex - 1; index >= 0; index--) {
                    if (currentTime > this.sentenceTimes[index]) return index;
                }
                return 0;
            }
        }

        private playerPlayingHandler = () => {
            if (this.currentState === PlayerState.Seeking) {
                this.currentState = PlayerState.Playing;
                return;
            }
            this.currentState = PlayerState.Playing;
            this.stateIndicator.toPlaying();
        }

        private playerPauseHandler = (ended: boolean) => {
            this.currentState = ended ? PlayerState.Stopped : PlayerState.Paused;
            this.stateIndicator.toPaused(ended);
        }

        private playerSeekingHandler = () => {
            const previousState = this.currentState;
            if (this.currentState === PlayerState.Playing) this.currentState = PlayerState.Seeking;
            else if (this.currentState === PlayerState.Stopped) this.currentState = PlayerState.Paused;
            this.stateIndicator.toSeeking(previousState);
        }

        private playerTimeUpdateHandler = (currentTime: number) => {
            this.currentSentenceIndex = this.getSentenceIndex(currentTime);
            if (this.currentSentenceIndex === this.lastSentenceIndex) return;
            this.container.readLine(this.lastSentenceIndex, this.currentSentenceIndex);
            this.lastSentenceIndex = this.currentSentenceIndex;
        }

        private stateIndicatorClickHandler = () => {
            this.player.switchState();
        }

        private containerMouseWheelHandler = (e: WheelEvent) => {
            let sentenceIndex: number;
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
        }

        private containerKeyDownHandler = (e: KeyboardEvent) => {
            const keyCode = Utils.Environment.browser === BrowserType.MSIE ? e.keyCode : e.which;
            if (keyCode === 38) {
                this.seek(this.currentSentenceIndex - 1);
            }
            else if (keyCode === 40) {
                this.seek(this.currentSentenceIndex + 1);
            }
            return false;
        }
    }
}