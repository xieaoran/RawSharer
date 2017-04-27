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
                    var Helpers = RawSharer.Common.Helpers;
                    var Container = (function () {
                        function Container(lyricsContainerId) {
                            this.containerFront = $("#" + lyricsContainerId);
                        }
                        Container.prototype.writeLine = function (index, content) {
                            var contentPara = document.createElement("p");
                            if (Helpers.Environment.browser === BrowserType.Firefox) {
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
                        Container.prototype.readLine = function (lastIndex, currentIndex) {
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
                        Container.prototype.attachEvent = function (mouseSeek, keyboardSeek) {
                            if (Helpers.Environment.browser === BrowserType.Firefox)
                                document.addEventListener("DOMMouseScroll", function (e) { return mouseSeek(e); }, false);
                            else {
                                document.onmousewheel = function (e) { return mouseSeek(e); };
                            }
                            document.onkeydown = function (e) { return keyboardSeek(e); };
                        };
                        return Container;
                    }());
                    Lyrics.Container = Container;
                })(Lyrics = Controls.Lyrics || (Controls.Lyrics = {}));
            })(Controls = UI.Controls || (UI.Controls = {}));
        })(UI = PlayBack.UI || (PlayBack.UI = {}));
    })(PlayBack = RawSharer.PlayBack || (RawSharer.PlayBack = {}));
})(RawSharer || (RawSharer = {}));
//# sourceMappingURL=Container.js.map