namespace RawSharer.PlayBack.UI.Page {
    import Playing = UI.Controls.Playing;
    import Lyrics = UI.Controls.Lyrics;

    export class PageAdapter {
        public stateIndicator: Playing.StateIndicator;
        public lyricsContainer: Lyrics.Container;

        public constructor(playerIconId: string, coverImgId: string, lyricsContainerId: string) {
            this.stateIndicator = new Playing.StateIndicator(playerIconId, coverImgId);
            this.lyricsContainer = new Lyrics.Container(lyricsContainerId);
        }
    }
}