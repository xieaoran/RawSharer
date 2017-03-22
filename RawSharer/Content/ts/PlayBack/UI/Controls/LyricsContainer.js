var RawSharer;
(function (RawSharer) {
    var PlayBack;
    (function (PlayBack) {
        var UI;
        (function (UI) {
            var Controls;
            (function (Controls) {
                var LyricsContainer = (function () {
                    function LyricsContainer(lyricsContainerId) {
                        this.containerFront = $("#" + lyricsContainerId);
                    }
                    LyricsContainer.prototype.writeLine = function (index, content) {
                        //TODO: Check if "const" causes bugs here.
                        var contentPara = document.createElement("p");
                        if (RawSharer.Helpers.Environment.browser === "Firefox") {
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
                    return LyricsContainer;
                }());
            })(Controls = UI.Controls || (UI.Controls = {}));
        })(UI = PlayBack.UI || (PlayBack.UI = {}));
    })(PlayBack = RawSharer.PlayBack || (RawSharer.PlayBack = {}));
})(RawSharer || (RawSharer = {}));
//# sourceMappingURL=LyricsContainer.js.map