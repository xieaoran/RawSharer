var RawSharer;
(function (RawSharer) {
    var Helpers;
    (function (Helpers) {
        var Environment = (function () {
            function Environment() {
            }
            Object.defineProperty(Environment, "browser", {
                get: function () {
                    if (this.innerBrowser == undefined) {
                        if (navigator.userAgent.indexOf("MSIE") >= 0)
                            this.innerBrowser = "MSIE";
                        else if (navigator.userAgent.indexOf("Firefox") >= 0)
                            this.innerBrowser = "Firefox";
                        else
                            this.innerBrowser = "WebKit";
                    }
                    return this.innerBrowser;
                },
                enumerable: true,
                configurable: true
            });
            return Environment;
        }());
        Helpers.Environment = Environment;
    })(Helpers = RawSharer.Helpers || (RawSharer.Helpers = {}));
})(RawSharer || (RawSharer = {}));
//# sourceMappingURL=Environment.js.map