var PlayBack;
(function (PlayBack) {
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
})(PlayBack || (PlayBack = {}));
//# sourceMappingURL=PageAdapter.js.map