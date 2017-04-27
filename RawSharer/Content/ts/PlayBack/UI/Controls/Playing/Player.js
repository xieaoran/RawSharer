var RawSharer;
(function (RawSharer) {
    var PlayBack;
    (function (PlayBack) {
        var UI;
        (function (UI) {
            var Playing;
            (function (Playing) {
                var Player = (function () {
                    function Player(playerId) {
                        this.playerFront = $("#" + playerId)[0];
                    }
                    Player.prototype.switchState = function () {
                        if (this.playerFront.paused)
                            this.playerFront.play();
                        else
                            this.playerFront.pause();
                    };
                    return Player;
                }());
                Playing.Player = Player;
            })(Playing = UI.Playing || (UI.Playing = {}));
        })(UI = PlayBack.UI || (PlayBack.UI = {}));
    })(PlayBack = RawSharer.PlayBack || (RawSharer.PlayBack = {}));
})(RawSharer || (RawSharer = {}));
//# sourceMappingURL=Player.js.map