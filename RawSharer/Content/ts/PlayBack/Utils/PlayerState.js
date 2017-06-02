var RawSharer;
(function (RawSharer) {
    var PlayBack;
    (function (PlayBack) {
        var Utils;
        (function (Utils) {
            var PlayerState;
            (function (PlayerState) {
                PlayerState[PlayerState["Playing"] = 0] = "Playing";
                PlayerState[PlayerState["Stopped"] = 1] = "Stopped";
                PlayerState[PlayerState["Paused"] = 2] = "Paused";
                PlayerState[PlayerState["Seeking"] = 3] = "Seeking";
            })(PlayerState = Utils.PlayerState || (Utils.PlayerState = {}));
        })(Utils = PlayBack.Utils || (PlayBack.Utils = {}));
    })(PlayBack = RawSharer.PlayBack || (RawSharer.PlayBack = {}));
})(RawSharer || (RawSharer = {}));
//# sourceMappingURL=PlayerState.js.map