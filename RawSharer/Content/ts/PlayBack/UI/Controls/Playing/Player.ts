namespace RawSharer.PlayBack.UI.Playing {

    export class Player {
        private playerFront: HTMLAudioElement;

        public constructor(playerId: string) {
            this.playerFront = $(`#${playerId}`)[0] as HTMLAudioElement;
        }

        public switchState(): void {
            if (this.playerFront.paused) this.playerFront.play();
            else this.playerFront.pause();
        }
    }
}