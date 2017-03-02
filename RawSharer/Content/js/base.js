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
    PlayBack: WinJS.UI.Pages.define("./Pages/PlayBack/Track/12F342B0-8288-49D6-815F-8B590073B164", {
        ready: function(element, options) {
            PlayBack.Load();
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
                    allowedExtensions: ["jpeg", "jpg", "gif", "png"]
                }
            });
        }
    })
});

WinJS.Namespace.defineWithParent(Base, "Navigation", {
    GoToIndex: function() {
        var index = new Base.Pages.Index(Base.Controls.PageRenderControl.DOM);
    },
    GoToPlayBack: function() {
        var playBack = new Base.Pages.PlayBack(Base.Controls.PageRenderControl.DOM);
    },
    GotoUpload: function() {
        var upload = new Base.Pages.Upload(Base.Controls.PageRenderControl.DOM);
    }
});