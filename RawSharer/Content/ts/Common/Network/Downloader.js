var RawSharer;
(function (RawSharer) {
    var Common;
    (function (Common) {
        var Network;
        (function (Network) {
            var Downloader = (function () {
                function Downloader() {
                }
                Downloader.download = function (url, callback) {
                    var request = new XMLHttpRequest();
                    request.open("GET", url, true);
                    request.responseType = "text";
                    request.onload = function () {
                        callback(request.responseText);
                    };
                };
                return Downloader;
            }());
            Network.Downloader = Downloader;
        })(Network = Common.Network || (Common.Network = {}));
    })(Common = RawSharer.Common || (RawSharer.Common = {}));
})(RawSharer || (RawSharer = {}));
//# sourceMappingURL=Downloader.js.map