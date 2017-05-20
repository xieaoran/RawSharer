namespace RawSharer.PlayBack.UI.Controls.Playing {
    import PlayerState = PlayBack.Utils.PlayerState;

    export class StateIndicator {
        private playerIconFront: JQuery;
        private coverImgFront: JQuery;

        private pauseTimeOut: number;

        public constructor(playerIconId: string, coverImgId: string) {
            this.playerIconFront = $(`#${playerIconId}`);
            this.coverImgFront = $(`#${coverImgId}`);
        }

        public toPlaying(): void {
            this.coverImgFront.removeClass("animation-paused");
            this.coverImgFront.addClass("animation-running");
            this.playerIconFront.removeClass("fa-stop fa-pause animation-running state-switching state-switched");
            this.playerIconFront.addClass("fa-play animation-running state-switching");
            setTimeout(() => {
                this.playerIconFront.removeClass("animation-running state-switching");
            }, 400);
        }

        public toPaused(ended: boolean): void {
            this.playerIconFront.removeClass("fa-play animation-running state-switching");
            if (ended) this.toStopped();
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

        public toStopped(): void {
            this.playerIconFront.addClass("fa-stop animation-running state-switching");
            this.coverImgFront.removeClass("animation-running");
            this.coverImgFront.addClass("animation-paused");
            setTimeout(() => {
                this.coverImgFront.removeClass("animation-running state-switching");
            }, 400);
        }

        public toSeeking(currentState: PlayerState): void {
            if (this.pauseTimeOut) clearTimeout(this.pauseTimeOut);
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

        public attachClick(click: () => void) {
            this.playerIconFront.click(click);
        }
    }
}