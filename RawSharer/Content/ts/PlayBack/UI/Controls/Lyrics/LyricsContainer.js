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
                    var BrowserType = RawSharer.Enums.BrowserType;
                    var LyricsContainer = (function () {
                        function LyricsContainer(lyricsContainerId) {
                            this.containerFront = $("#" + lyricsContainerId);
                        }
                        LyricsContainer.prototype.writeLine = function (index, content) {
                            var contentPara = document.createElement("p");
                            if (RawSharer.Helpers.Environment.browser === BrowserType.Firefox) {
                                if (content === undefined)
                                    contentPara.innerHTML = "<br/>";
                                else
                                    contentPara.innerHTML = content;
                            }
                            else
                                contentPara.innerHTML = content;
                            contentPara.id = "lyric" + index;
                            this.containerFront.append(contentPara);
                        };
                        LyricsContainer.prototype.readLine = function (lastIndex, currentIndex) {
                            var lastLine = $("#lyric" + lastIndex);
                            var currentLine = $("#lyric" + currentIndex);
                            lastLine.stop();
                            lastLine.css("font-weight", "normal");
                            lastLine.animate({ "color": "white" }, "fast");
                            currentLine.stop();
                            currentLine.css("font-weight", "bold");
                            currentLine.animate({ "color": "#FFC107" }, "fast");
                            this.containerFront.stop();
                            this.containerFront.animate({ "top": 140 - currentIndex * 35 }, "fast", "easeOutCubic");
                        };
                        LyricsContainer.prototype.attachEvent = function (mouseSeek, keyboardSeek) {
                            if (RawSharer.Helpers.Environment.browser === BrowserType.Firefox)
                                document.addEventListener("DOMMouseScroll", function (e) { return mouseSeek(e); }, false);
                            else {
                                document.onmousewheel = function (e) { return mouseSeek(e); };
                            }
                            document.onkeydown = function (e) { return keyboardSeek(e); };
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