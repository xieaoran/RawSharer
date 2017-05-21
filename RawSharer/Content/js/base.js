WinJS.Namespace.define("Base", {
    Control: WinJS.Class.define(function (id) {
        this.Id = id;
        this.JQuerySelector = $("#" + id);
        this.DOM = this.JQuerySelector[0];
    }),
    Load: function () {
        WinJS.UI.processAll().done(function () {
            var splitView = document.querySelector(".splitView").winControl;
        });
    }
});

WinJS.Namespace.defineWithParent(Base, "Controls", {
    PageRenderControl: new Base.Control("pageRenderControl")
});

WinJS.Namespace.defineWithParent(Base, "Pages", {
    Index: WinJS.UI.Pages.define("./Pages/Home/Index"),
    PlayBack: WinJS.UI.Pages.define("./Pages/PlayBack/TrackVersion/958E478E-502B-432B-BCBB-E9CE682F85A9", {
        ready: function () {
            var player = new RawSharer.PlayBack.UI.Controls.Playing.Player("player");
            var stateIndicator = new RawSharer.PlayBack.UI.Controls.Playing.StateIndicator("player-icon", "cover-img");
            var container = new RawSharer.PlayBack.UI.Controls.Lyrics.LyricsContainer("lyrics-container", "lyrics-border");
            var controller = new RawSharer.PlayBack.Core.PlayBackController(player, stateIndicator, container);
        }
    }),
    Upload: WinJS.UI.Pages.define("./Pages/Upload", {})
});

WinJS.Namespace.defineWithParent(Base, "Navigation", {
    GoToIndex: function () {
        var index = new Base.Pages.Index(Base.Controls.PageRenderControl.DOM);
    },
    GoToPlayBack: function () {
        var playBack = new Base.Pages.PlayBack(Base.Controls.PageRenderControl.DOM);
    },
    GotoUpload: function () {
        var upload = new Base.Pages.Upload(Base.Controls.PageRenderControl.DOM);
    }
});