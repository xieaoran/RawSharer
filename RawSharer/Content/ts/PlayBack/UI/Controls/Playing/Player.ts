namespace RawSharer.PlayBack.UI.Controls.Playing {

    export class Player {
        private readonly playerFront: HTMLAudioElement;

        public constructor(playerId: string) {
            this.playerFront = document.getElementById(playerId) as HTMLAudioElement;

        }

        public switchState(): void {
            if (this.playerFront.paused) this.playerFront.play();
            else this.playerFront.pause();
        }

        public seek(seekTime: number): void {
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

        public attachPlaying(playing: () => void): void {
            this.playerFront.onplaying = playing;
        }

        public attachPause(pause: (arg: boolean) => void): void {
            this.playerFront.onpause = () => pause(this.playerFront.ended);
        }

        public attachSeeking(seeking: () => void): void {
            this.playerFront.onseeking = seeking;
        }

        public attachTimeUpdate(timeUpdate: (arg: number) => void): void {
            this.playerFront.ontimeupdate = () => timeUpdate(this.playerFront.currentTime);
        }
    }
}