namespace RawSharer.PlayBack.UI.Controls.Lyrics {
    import BrowserType = Enums.BrowserType;
    import Helpers = Common.Helpers;

    export class Container {
        private containerFront: JQuery;

        public constructor(lyricsContainerId: string) {
            this.containerFront = $(`#${lyricsContainerId}`);
        }

        public writeLine(index: number, content: string): void {
            const contentPara = document.createElement("p");
            if (Helpers.Environment.browser === BrowserType.Firefox) {
                if (content === undefined) contentPara.innerHTML = "<br/>";
                else contentPara.innerHTML = content;
            }
            else contentPara.innerHTML = content;
            contentPara.id = `lyric${index}`;
            this.containerFront.append(contentPara);
        }

        public readLine(lastIndex: number, currentIndex: number): void {
            const lastLine = $(`#lyric${lastIndex}`);
            const currentLine = $(`#lyric${currentIndex}`);
            lastLine.stop();
            lastLine.css("font-weight", "normal");
            lastLine.animate({ "color": "white" }, "fast");
            currentLine.stop();
            currentLine.css("font-weight", "bold");
            currentLine.animate({ "color": "#FFC107" }, "fast");
            this.containerFront.stop();
            this.containerFront.animate({ "top": 140 - currentIndex * 35 }, "fast", "easeOutCubic");
        }

        public attachEvent(mouseSeek: (e: WheelEvent) => void, keyboardSeek: (e: KeyboardEvent) => void) {
            if (Helpers.Environment.browser === BrowserType.Firefox)
                document.addEventListener("DOMMouseScroll", (e: WheelEvent) => mouseSeek(e), false);
            else {
                document.onmousewheel = (e: WheelEvent) => mouseSeek(e);
            }
            document.onkeydown = (e: KeyboardEvent) => keyboardSeek(e);
        }
    }
}