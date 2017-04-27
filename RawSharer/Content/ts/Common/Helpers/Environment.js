var RawSharer;
(function (RawSharer) {
    var Common;
    (function (Common) {
        var Helpers;
        (function (Helpers) {
            var BrowserType = RawSharer.Enums.BrowserType;
            var Environment = (function () {
                function Environment() {
                }
                Object.defineProperty(Environment, "browser", {
                    get: function () {
                        if (this.innerBrowser == undefined) {
                            if (navigator.userAgent.indexOf("MSIE") >= 0)
                                this.innerBrowser = BrowserType.MSIE;
                            else if (navigator.userAgent.indexOf("Firefox") >= 0)
                                this.innerBrowser = BrowserType.Firefox;
                            else
                                this.innerBrowser = BrowserType.Webkit;
                        }
                        return this.innerBrowser;
                    },
                    enumerable: true,
                    configurable: true
                });
                return Environment;
            }());
            Helpers.Environment = Environment;
        })(Helpers = Common.Helpers || (Common.Helpers = {}));
    })(Common = RawSharer.Common || (RawSharer.Common = {}));
})(RawSharer || (RawSharer = {}));
//# sourceMappingURL=Environment.js.map