var RawSharer;
(function (RawSharer) {
    var PlayBack;
    (function (PlayBack) {
        var UI;
        (function (UI) {
            var Controls;
            (function (Controls) {
                var Lyrics;
                (function (Lyrics) {
                    var Utils = RawSharer.Common.Utils;
                    var BrowserType = RawSharer.Common.Utils.BrowserType;
                    class LyricsContainer {
                        constructor(containerId, borderId) {
                            this.containerFront = $(`#${containerId}`);
                            this.borderHeight = $(`#${borderId}`).height();
                            this.containerFront.css("margin-top", this.borderHeight * 0.4);
                        }
                        readLine(lastIndex, currentIndex) {
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
                        getSentenceTimes() {
                            const sentenceTimes = [];
                            this.containerFront.children("p").each((index, elem) => {
                                sentenceTimes.push($(elem).data("time"));
                            });
                            return sentenceTimes;
                        }
                        attachMouseWheel(mouseWheel) {
                            if (Utils.Environment.browser === BrowserType.Firefox)
                                document.addEventListener("DOMMouseScroll", mouseWheel, false);
                            else {
                                document.onmousewheel = mouseWheel;
                            }
                        }
                        attachKeyDown(keyDown) {
                            document.onkeydown = keyDown;
                        }
                    }
                    Lyrics.LyricsContainer = LyricsContainer;
                })(Lyrics = Controls.Lyrics || (Controls.Lyrics = {}));
            })(Controls = UI.Controls || (UI.Controls = {}));
        })(UI = PlayBack.UI || (PlayBack.UI = {}));
    })(PlayBack = RawSharer.PlayBack || (RawSharer.PlayBack = {}));
})(RawSharer || (RawSharer = {}));
//# sourceMappingURL=LyricsContainer.js.map