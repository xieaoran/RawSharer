WinJS.Namespace.define("Base", {
    Control: WinJS.Class.define(function (id) {
        this.ID = id;
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
    PlayBack: WinJS.UI.Pages.define("./Pages/PlayBack/TrackVersion/E202D41C-364D-4DD2-9751-116B3541F1CE", {
        ready: function () {
            var player = new RawSharer.PlayBack.UI.Controls.Playing.Player("player");
            var stateIndicator = new RawSharer.PlayBack.UI.Controls.Playing.StateIndicator("player-icon", "cover-img");
            var container = new RawSharer.PlayBack.UI.Controls.Lyrics.LyricsContainer("lyrics-container", "lyrics-border");
            var controller = new RawSharer.PlayBack.Core.PlayBackController(player, stateIndicator, container);
        }
    }),
    Upload: WinJS.UI.Pages.define("./Pages/Upload",
        {
            ready: function (element, options) {
                Upload.Load();
                $("#uploader-container").fineUploader({
                    template: "qq-template",
                    request: {
                        endpoint: "/server/uploads"
                    },
                    thumbnails: {
                        placeholders: {
                            waitingPath: "/Content/frameworks/fine-uploader/placeholders/waiting-generic.png",
                            notAvailablePath: "/Content/frameworks/fine-uploader/placeholders/not_available-generic.png"
                        }
                    },
                    validation: {
                        allowedExtensions: ["wav", "flac", "ape", "tak", "tta"]
                    }
                });
            }
        })
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