namespace RawSharer.PlayBack.Core.Lyrics {
    export class Parser {
        private pattern: RegExp;

        public constructor(pattern: RegExp) {
            this.pattern = pattern;
        }

        public parse(lyrics: string): [number, string][] {
            let lines = lyrics.split("\n");
            const result: [number, string][] = [];
            while (!this.pattern.test(lines[0])) {
                lines = lines.slice(1);
            }
            lines[lines.length - 1].length === 0 && lines.pop();
            lines.forEach(line => {
                const times = line.match(this.pattern),
                    value = line.replace(this.pattern, "");
                times.forEach(time => {
                    const t = time.slice(1, -1).split(":");
                    result.push([parseInt(t[0], 10) * 60 + parseFloat(t[1]), value]);
                });
            });
            result.sort((a, b) => a[0] - b[0]);
            return result;
        }
    }
}