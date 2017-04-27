namespace RawSharer.Common.Helpers {
    import BrowserType = Enums.BrowserType;

    export class Environment {
        private static innerBrowser: BrowserType;

        public static get browser(): BrowserType {
            if (this.innerBrowser == undefined) {
                if (navigator.userAgent.indexOf("MSIE") >= 0) this.innerBrowser = BrowserType.MSIE;
                else if (navigator.userAgent.indexOf("Firefox") >= 0) this.innerBrowser = BrowserType.Firefox;
                else this.innerBrowser = BrowserType.Webkit;
            }
            return this.innerBrowser;
        }
    }
}