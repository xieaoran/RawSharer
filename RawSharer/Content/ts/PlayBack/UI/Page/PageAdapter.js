var RawSharer;
(function (RawSharer) {
    var PlayBack;
    (function (PlayBack) {
        var UI;
        (function (UI) {
            var Page;
            (function (Page) {
                var Playing = UI.Controls.Playing;
                var Lyrics = UI.Controls.Lyrics;
                var PageAdapter = (function () {
                    function PageAdapter(playerIconId, coverImgId, lyricsContainerId) {
                        this.stateIndicator = new Playing.StateIndicator(playerIconId, coverImgId);
                        this.lyricsContainer = new Lyrics.Container(lyricsContainerId);
                    }
                    return PageAdapter;
                }());
                Page.PageAdapter = PageAdapter;
            })(Page = UI.Page || (UI.Page = {}));
        })(UI = PlayBack.UI || (PlayBack.UI = {}));
    })(PlayBack = RawSharer.PlayBack || (RawSharer.PlayBack = {}));
})(RawSharer || (RawSharer = {}));
//# sourceMappingURL=PageAdapter.js.map