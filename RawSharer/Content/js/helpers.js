WinJS.Namespace.define("Helpers");

WinJS.Namespace.defineWithParent(Helpers, "Browser", {
    GetBrowser: function() {
        if (navigator.userAgent.indexOf("MSIE") >= 0) return "MSIE";
        else if (navigator.userAgent.indexOf("Firefox") >= 0) return "Firefox";
        else return undefined;
    }
});