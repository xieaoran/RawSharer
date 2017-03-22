var PlayBack;
(function (PlayBack) {
    var UI;
    (function (UI) {
        var PageAdapter = (function () {
            function PageAdapter(playerIconId, coverImgId, lyricContainerId) {
                this.playerIcon = $("#" + playerIconId);
                this.coverImg = $("#" + coverImgId);
                this.lyricContainer = $("#" + lyricContainerId);
            }
            PageAdapter.prototype.bindCore = function (core) {
                this.core = core;
            };
            return PageAdapter;
        }());
    })(UI = PlayBack.UI || (PlayBack.UI = {}));
})(PlayBack || (PlayBack = {}));
//# sourceMappingURL=PageAdapter.js.map