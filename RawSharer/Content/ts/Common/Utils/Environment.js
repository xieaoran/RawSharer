var RawSharer;
(function (RawSharer) {
    var Common;
    (function (Common) {
        var Utils;
        (function (Utils) {
            class Environment {
                static get browser() {
                    if (this.innerBrowser == undefined) {
                        if (navigator.userAgent.indexOf("MSIE") >= 0)
                            this.innerBrowser = Utils.BrowserType.MSIE;
                        else if (navigator.userAgent.indexOf("Firefox") >= 0)
                            this.innerBrowser = Utils.BrowserType.Firefox;
                        else
                            this.innerBrowser = Utils.BrowserType.Webkit;
                    }
                    return this.innerBrowser;
                }
            }
            Utils.Environment = Environment;
        })(Utils = Common.Utils || (Common.Utils = {}));
    })(Common = RawSharer.Common || (RawSharer.Common = {}));
})(RawSharer || (RawSharer = {}));
//# sourceMappingURL=Environment.js.map