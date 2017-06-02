import "es6-promise/auto";

namespace RawSharer.Common.Network {
    export class Downloader {
        public static async downloadAsync(url: string) : Promise<string> {
            const promise = new Promise<string>(resolve => {
                const request = new XMLHttpRequest();
                request.open("GET", url, true);
                request.responseType = "text";
                request.onload = () => {
                    resolve(request.responseText);
                }
            });
            return promise;
        }
    }
}