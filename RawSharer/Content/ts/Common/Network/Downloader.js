"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
Object.defineProperty(exports, "__esModule", { value: true });
require("es6-promise/auto");
var RawSharer;
(function (RawSharer) {
    var Common;
    (function (Common) {
        var Network;
        (function (Network) {
            class Downloader {
                static downloadAsync(url) {
                    return __awaiter(this, void 0, void 0, function* () {
                        const promise = new Promise(resolve => {
                            const request = new XMLHttpRequest();
                            request.open("GET", url, true);
                            request.responseType = "text";
                            request.onload = () => {
                                resolve(request.responseText);
                            };
                        });
                        return promise;
                    });
                }
            }
            Network.Downloader = Downloader;
        })(Network = Common.Network || (Common.Network = {}));
    })(Common = RawSharer.Common || (RawSharer.Common = {}));
})(RawSharer || (RawSharer = {}));
//# sourceMappingURL=Downloader.js.map