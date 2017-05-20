namespace RawSharer.PlayBack.UI.Controls.Lyrics {
    import Utils = Common.Utils;
    import BrowserType = Common.Utils.BrowserType;

    export class LyricsContainer {
        private readonly containerFront: JQuery;
        private readonly borderHeight: number;

        public constructor(containerId: string, borderId: string) {
            this.containerFront = $(`#${containerId}`);
            this.borderHeight = $(`#${borderId}`).height();
            this.containerFront.css("margin-top", this.borderHeight * 0.4);
        }

        public readLine(lastIndex: number, currentIndex: number): void {
            const lastLine = $(`#lyrics${lastIndex}`);
            const currentLine = $(`#lyrics${currentIndex}`);
            lastLine.stop();
            lastLine.css("font-weight", "normal");
            lastLine.animate({ "color": "white" }, "fast");
            currentLine.stop();
            currentLine.css("font-weight", "bold");
            currentLine.animate({ "color": "#FFC107" }, "fast");

            const lineOffsetTop = currentLine[0].offsetTop;
            const lineHeight = currentLine.height();
            const containerOffsetTop = this.borderHeight * 0.4 - lineOffsetTop - lineHeight * 0.5;

            this.containerFront.stop();
            this.containerFront.animate({ "margin-top": containerOffsetTop }, "fast", "easeOutCubic");
        }

        public getSentenceTimes(): number[] {
            const sentenceTimes: number[] = [];
            this.containerFront.children("p").each((index, elem) => {
                sentenceTimes.push($(elem).data("time"));
            });
            return sentenceTimes;
        }

        public attachMouseWheel(mouseWheel: (arg: WheelEvent) => boolean) {
            if (Utils.Environment.browser === BrowserType.Firefox)
                document.addEventListener("DOMMouseScroll", mouseWheel, false);
            else {
                document.onmousewheel = mouseWheel;
            }
        }

        public attachKeyDown(keyDown: (arg: KeyboardEvent) => boolean): void {
            document.onkeydown = keyDown;
        }
    }
}