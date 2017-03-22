var RawSharer;
(function (RawSharer) {
    var Helpers;
    (function (Helpers) {
        var Browser = (function () {
            function Browser() {
            }
            Object.defineProperty(Browser, "browser", {
                get: function () {
                    if (this._browser == undefined) {
                        if (navigator.userAgent.indexOf("MSIE") >= 0)
                            this._browser = "MSIE";
                        else if (navigator.userAgent.indexOf("Firefox") >= 0)
                            this._browser = "Firefox";
                        else
                            this._browser = "WebKit";
                    }
                    return this._browser;
                },
                enumerable: true,
                configurable: true
            });
            Browser.getBrowser = function () {
                if (navigator.userAgent.indexOf("MSIE") >= 0)
                    return "MSIE";
                else if (navigator.userAgent.indexOf("Firefox") >= 0)
                    return "Firefox";
                else
                    return undefined;
            };
            return Browser;
        }());
        Helpers.Browser = Browser;
    })(Helpers = RawSharer.Helpers || (RawSharer.Helpers = {}));
})(RawSharer || (RawSharer = {}));
//# sourceMappingURL=Browser.js.map