namespace RawSharer.PlayBack.UI {
    class PageAdapter {
        private core: any;

        private playerIcon: JQuery;
        private coverImg: JQuery;
        private lyricContainer: JQuery;

        public constructor(playerIconId: string, coverImgId: string, lyricContainerId: string) {
            this.playerIcon = $(`#${playerIconId}`);
            this.coverImg = $(`#${coverImgId}`);
            this.lyricContainer = $(`#${lyricContainerId}`);
        }

        public bindCore(core: any) {
            this.core = core;
        }
    }
}