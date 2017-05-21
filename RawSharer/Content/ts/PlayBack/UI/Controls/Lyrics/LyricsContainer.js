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
                    var LyricsContainer = (function () {
                        function LyricsContainer(containerId, borderId) {
                            this.containerFront = $("#" + containerId);
                            this.borderHeight = $("#" + borderId).height();
                            this.containerFront.css("margin-top", this.borderHeight * 0.4);
                        }
                        LyricsContainer.prototype.readLine = function (lastIndex, currentIndex) {
                            var lastLine = $("#lyrics" + lastIndex);
                            var currentLine = $("#lyrics" + currentIndex);
                            lastLine.stop();
                            lastLine.css("font-weight", "normal");
                            lastLine.animate({ "color": "white" }, "fast");
                            currentLine.stop();
                            currentLine.css("font-weight", "bold");
                            currentLine.animate({ "color": "#FFC107" }, "fast");
                            var lineOffsetTop = currentLine[0].offsetTop;
                            var lineHeight = currentLine.height();
                            var containerOffsetTop = this.borderHeight * 0.4 - lineOffsetTop - lineHeight * 0.5;
                            this.containerFront.stop();
                            this.containerFront.animate({ "margin-top": containerOffsetTop }, "fast", "easeOutCubic");
                        };
                        LyricsContainer.prototype.getSentenceTimes = function () {
                            var sentenceTimes = [];
                            this.containerFront.children("p").each(function (index, elem) {
                                sentenceTimes.push($(elem).data("time"));
                            });
                            return sentenceTimes;
                        };
                        LyricsContainer.prototype.attachMouseWheel = function (mouseWheel) {
                            if (Utils.Environment.browser === BrowserType.Firefox)
                                document.addEventListener("DOMMouseScroll", mouseWheel, false);
                            else {
                                document.onmousewheel = mouseWheel;
                            }
                        };
                        LyricsContainer.prototype.attachKeyDown = function (keyDown) {
                            document.onkeydown = keyDown;
                        };
                        return LyricsContainer;
                    }());
                    Lyrics.LyricsContainer = LyricsContainer;
                })(Lyrics = Controls.Lyrics || (Controls.Lyrics = {}));
            })(Controls = UI.Controls || (UI.Controls = {}));
        })(UI = PlayBack.UI || (PlayBack.UI = {}));
    })(PlayBack = RawSharer.PlayBack || (RawSharer.PlayBack = {}));
})(RawSharer || (RawSharer = {}));
//# sourceMappingURL=LyricsContainer.js.map