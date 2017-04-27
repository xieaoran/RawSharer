var RawSharer;
(function (RawSharer) {
    var PlayBack;
    (function (PlayBack) {
        var Core;
        (function (Core) {
            var Lyrics;
            (function (Lyrics) {
                var Parser = (function () {
                    function Parser(pattern) {
                        this.pattern = pattern;
                    }
                    Parser.prototype.parse = function (lyrics) {
                        var _this = this;
                        var lines = lyrics.split("\n");
                        var result = [];
                        while (!this.pattern.test(lines[0])) {
                            lines = lines.slice(1);
                        }
                        lines[lines.length - 1].length === 0 && lines.pop();
                        lines.forEach(function (line) {
                            var times = line.match(_this.pattern), value = line.replace(_this.pattern, "");
                            times.forEach(function (time) {
                                var t = time.slice(1, -1).split(":");
                                result.push([parseInt(t[0], 10) * 60 + parseFloat(t[1]), value]);
                            });
                        });
                        result.sort(function (a, b) { return a[0] - b[0]; });
                        return result;
                    };
                    return Parser;
                }());
                Lyrics.Parser = Parser;
            })(Lyrics = Core.Lyrics || (Core.Lyrics = {}));
        })(Core = PlayBack.Core || (PlayBack.Core = {}));
    })(PlayBack = RawSharer.PlayBack || (RawSharer.PlayBack = {}));
})(RawSharer || (RawSharer = {}));
//# sourceMappingURL=Parser.js.map