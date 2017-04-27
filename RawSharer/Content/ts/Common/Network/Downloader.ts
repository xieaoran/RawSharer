namespace RawSharer.Common.Network {
    export class Downloader {
        public static download(url: string, callback: (response: string) => void) {
            const request = new XMLHttpRequest();
            request.open("GET", url, true);
            request.responseType = "text";
            request.onload = () => {
                callback(request.responseText);
            }
        }
    }
}