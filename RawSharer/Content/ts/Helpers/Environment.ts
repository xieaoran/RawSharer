namespace RawSharer.Helpers {
    export class Environment {
        private static innerBrowser: string;
        public static get browser(): string {
            if (this.innerBrowser == undefined) {
                if (navigator.userAgent.indexOf("MSIE") >= 0) this.innerBrowser = "MSIE";
                else if (navigator.userAgent.indexOf("Firefox") >= 0) this.innerBrowser = "Firefox";
                else this.innerBrowser = "WebKit";
            }
            return this.innerBrowser;

        }
    }
}