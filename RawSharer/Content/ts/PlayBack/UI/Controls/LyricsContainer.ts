namespace RawSharer.PlayBack.UI.Controls {
    class LyricsContainer{
        private containerFront: JQuery;

        public constructor(lyricsContainerId: string) {
            this.containerFront = $(`#${lyricsContainerId}`);
        }

        public writeLine(index: number, content: string) {
            //TODO: Check if "const" causes bugs here.
            const contentPara = document.createElement("p");
            if (Helpers.Environment.browser === "Firefox") {
                if (content === undefined) contentPara.innerHTML = "<br/>";
                else contentPara.innerHTML = content;
            }
            else contentPara.innerHTML = content;
            contentPara.id = `lyric${index}`;
            this.containerFront.append(contentPara);
        }

        public readLine(lastIndex: number, currentIndex: number) {
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
    }
}