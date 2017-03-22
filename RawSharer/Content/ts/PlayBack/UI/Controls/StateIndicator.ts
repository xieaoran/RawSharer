namespace RawSharer.PlayBack.UI.Controls {
    import PlayerState = Enums.PlayerState;

    class StateIndicator {
        private playerIconFront: JQuery;
        private coverImgFront: JQuery;

        private pauseTimeOut: number;

        public constructor(playerIconId: string, coverImgId: string) {
            this.playerIconFront = $(`#${playerIconId}`);
            this.coverImgFront = $(`#${coverImgId}`);
        }

        public playing() {
            this.coverImgFront.removeClass("animation-paused");
            this.coverImgFront.addClass("animation-running");
            this.playerIconFront.removeClass("fa-stop fa-pause animation-running state-switching state-switched");
            this.playerIconFront.addClass("fa-play animation-running state-switching");
            setTimeout(() => {
                this.playerIconFront.removeClass("animation-running state-switching");
            }, 400);
        }

        public paused(ended: boolean) {
            this.playerIconFront.removeClass("fa-play animation-running state-switching");
            if (ended) this.stopped();
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

        public stopped() {
            this.playerIconFront.addClass("fa-stop animation-running state-switching");
            this.coverImgFront.removeClass("animation-running");
            this.coverImgFront.addClass("animation-paused");
            setTimeout(() => {
                this.coverImgFront.removeClass("animation-running state-switching");
            }, 400);
        }

        public seeking(currentState: PlayerState) {
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
    }
}