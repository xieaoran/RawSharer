var RawSharer;
(function (RawSharer) {
    var PlayBack;
    (function (PlayBack) {
        var UI;
        (function (UI) {
            var Page;
            (function (Page) {
                var PlayState = UI.Controls.PlayState;
                var Lyrics = UI.Controls.Lyrics;
                var PageAdapterFactory = (function () {
                    function PageAdapterFactory() {
                    }
                    PageAdapterFactory.prototype.produce = function () {
                        var newAdapter = new Page.PageAdapter();
                        newAdapter.stateIndicator = new PlayState.Indicator(this.playerIconId, this.coverImgId);
                        newAdapter.lyricsContainer = new Lyrics.Container(this.lyricsContainerId);
                        return newAdapter;
                    };
                    return PageAdapterFactory;
                }());
                Page.PageAdapterFactory = PageAdapterFactory;
            })(Page = UI.Page || (UI.Page = {}));
        })(UI = PlayBack.UI || (PlayBack.UI = {}));
    })(PlayBack = RawSharer.PlayBack || (RawSharer.PlayBack = {}));
})(RawSharer || (RawSharer = {}));
//# sourceMappingURL=PageAdapterFactory.js.map