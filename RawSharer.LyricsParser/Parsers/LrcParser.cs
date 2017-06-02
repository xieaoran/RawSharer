using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RawSharer.LyricsParser.Models;
using RawSharer.LyricsParser.Settings;
using RawSharer.LyricsParser.Utils;

namespace RawSharer.LyricsParser.Parsers
{
    public class LrcParser
    {
        private static readonly char[] OffsetNexts = { 'f', 'f', 's', 'e', 't' };
        private static readonly StringBuilder TimeBuilder = new StringBuilder(3);
        private static readonly LyricsSentenceComparer Comparer = new LyricsSentenceComparer();

        private LrcParserInnerSettings InnerSettings { get; }

        /// <summary>
        /// Creates a new parser using default settings.
        /// </summary>
        public LrcParser() : this(new LrcParserSettings())
        {

        }

        /// <summary>
        /// Creates a new parser using custom specified settings.
        /// </summary>
        /// <param name="settings">Parser Settings</param>
        public LrcParser(LrcParserSettings settings)
        {
            InnerSettings = new LrcParserInnerSettings(settings);
        }

        /// <summary>
        /// Parse lyrics from LRC-format lyrics string.
        /// </summary>
        /// <param name="lrcString">Lyrics String</param>
        /// <returns>Parsed Lyrics</returns>
        public async Task<ParsedLyrics> ParseAsync(string lrcString)
        {
            using (var reader = new StringReader(lrcString))
            {
                return await ParseAsync(reader);
            }
        }

        /// <summary>
        /// Parse lyrics from LRC-format lyrics source stream.
        /// </summary>
        /// <param name="lrcStream">Lyrics Source Stream</param>
        /// <returns>Parsed Lyrics</returns>
        public async Task<ParsedLyrics> ParseAsync(Stream lrcStream)
        {
            using (var reader = new StreamReader(lrcStream, Encoding.UTF8, false, 1024, true))
            {
                return await ParseAsync(reader);
            }
        }

        /// <summary>
        /// Parse lyrics from TextReader.
        /// </summary>
        /// <param name="reader">TextReader</param>
        /// <returns>Parsed Lyrics</returns>
        public async Task<ParsedLyrics> ParseAsync(TextReader reader)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var result = new ParsedLyrics();

                    while (ParseLine(result, reader))
                    {
                    }

                    if (InnerSettings.PostProcess) PostProcess(result);
                    return result;
                }
                catch (Exception exception)
                {
                    throw new ParseException(Resources.ExceptionMessages.GenericFailure, exception);
                }
            });

        }

        private bool ParseLine(ParsedLyrics lyrics, TextReader reader)
        {
            var leftBracket = reader.Read();
            if (leftBracket == -1) return false;
            else if (leftBracket == '\n') return true;
            else if (leftBracket != '[') goto ParseError;

            var firstChar = (char)reader.Read();
            if (firstChar == -1) return false;
            else if (leftBracket == '\n') return true;
            else if (char.IsDigit(firstChar))
            {
                if (!InnerSettings.MetaDataOnly)
                    ParseSentence(lyrics, reader, firstChar);
                else return false;
            }
            else ParseMetaData(lyrics, reader, firstChar);
            return true;

            ParseError:
            reader.ReadLine();
            return true;
        }

        private static void ParseSentence(ParsedLyrics lyrics, TextReader reader, char firstChar)
        {
            var startTimes = new List<TimeSpan>(4);

            TimeBuilder.Clear();
            TimeBuilder.Append(firstChar);
            var parsedTime = ParseTime(reader);
            if (!parsedTime.HasValue) goto IgnoreLine;
            startTimes.Add(parsedTime.Value);

            int nextChar;
            while ((nextChar = reader.Read()) == '[')
            {
                TimeBuilder.Clear();
                parsedTime = ParseTime(reader);
                if (!parsedTime.HasValue) goto IgnoreLine;
                startTimes.Add(parsedTime.Value);
            }

            var value = nextChar == -1 || nextChar == '\n' ?
                string.Empty : (char)nextChar + reader.ReadLine().Trim();
            foreach (var startTime in startTimes)
            {
                lyrics.Sentences.Add(new ParsedSentence { StartTime = startTime, Value = value });
            }
            return;

            IgnoreLine:
            reader.ReadLine();
        }
        private static void ParseMetaData(ParsedLyrics lyrics, TextReader reader, char firstChar)
        {
            MetaType metaType;
            if (firstChar == 'a')
            {
                var secondChar = reader.Read();
                if (secondChar == -1 || secondChar == '\n') return;
                else if (secondChar == 'l') metaType = MetaType.Album;
                else if (secondChar == 'r') metaType = MetaType.Artist;
                else if (secondChar == 'u') metaType = MetaType.Author;
                else goto IgnoreLine;
            }
            else if (firstChar == 'b')
            {
                var secondChar = reader.Read();
                if (secondChar == -1 || secondChar == '\n') return;
                else if (secondChar == 'y') metaType = MetaType.LrcCreator;
                else goto IgnoreLine;
            }
            else if (firstChar == 'o')
            {
                var nextFiveChars = new char[5];
                reader.Read(nextFiveChars, 0, 5);
                if (nextFiveChars.SequenceEqual(OffsetNexts)) metaType = MetaType.Offset;
                else goto IgnoreLine;
            }
            else if (firstChar == 't')
            {
                var secondChar = reader.Read();
                if (secondChar == 'i') metaType = MetaType.Title;
                else goto IgnoreLine;
            }
            else if (firstChar == -1 || firstChar == '\n') return;
            else goto IgnoreLine;

            var nextChar = reader.Read();
            if (nextChar == -1 || nextChar == '\n') return;
            else if (nextChar != ':') goto IgnoreLine;

            var line = reader.ReadLine();
            if (line == null) return;

            var value = line.Trim();
            if (value[value.Length - 1] == ']') value = value.Substring(0, value.Length - 1);

            if (metaType == MetaType.Album) lyrics.Album = value;
            else if (metaType == MetaType.Artist) lyrics.Artist = value;
            else if (metaType == MetaType.Author) lyrics.Author = value;
            else if (metaType == MetaType.LrcCreator) lyrics.LrcCreator = value;
            else if (metaType == MetaType.Offset) lyrics.OffsetMs = int.Parse(value);
            else if (metaType == MetaType.Title) lyrics.Title = value;
            return;

            IgnoreLine:
            reader.ReadLine();
        }

        private static TimeSpan? ParseTime(TextReader reader)
        {
            var flag = 0;
            int minute = 0, second = 0;
            char nextChar;
            while ((nextChar = (char)reader.Read()) != ']')
            {
                if (flag == 0 && nextChar == ':')
                {
                    if (!int.TryParse(TimeBuilder.ToString(), out minute)) goto IgnoreTime;
                    TimeBuilder.Clear();
                    flag++;
                }
                else if (flag == 1 && (nextChar == '.' || nextChar == ':'))
                {
                    if (!int.TryParse(TimeBuilder.ToString(), out second)) goto IgnoreTime;
                    TimeBuilder.Clear();
                    flag++;
                }
                else TimeBuilder.Append(nextChar);
            }
            if (flag == 1)
            {
                if (!int.TryParse(TimeBuilder.ToString(), out second)) goto IgnoreTime;
                return new TimeSpan(0, minute, second);
            }
            else if (flag == 2)
            {
                if (!int.TryParse(TimeBuilder.ToString(), out int millisecond)) goto IgnoreTime;
                for (var i = TimeBuilder.Length; i < 3; i++)
                {
                    millisecond *= 10;
                }
                return new TimeSpan(0, 0, minute, second, millisecond);
            }

            IgnoreTime:
            return null;
        }

        private static void PostProcess(ParsedLyrics lyrics)
        {
            lyrics.Sentences.Sort(Comparer);

            var hasOffset = lyrics.OffsetMs != 0;
            var offset = TimeSpan.FromMilliseconds(lyrics.OffsetMs);

            if (lyrics.Sentences.Count == 0) return;
            var previous = lyrics.Sentences[0];
            previous.Sequence = 0;
            if (hasOffset)
            {
                previous.StartTime = previous.StartTime.Add(offset);
                if (previous.StartTime < TimeSpan.Zero)
                    throw new ParseException(Resources.ExceptionMessages.InvalidOffset);
            }
            for (var i = 1; i < lyrics.Sentences.Count; i++)
            {
                var current = lyrics.Sentences[i];
                current.Sequence = i;
                if (hasOffset) current.StartTime = current.StartTime.Add(offset);

                previous.EndTime = current.StartTime;
                previous.Duration = previous.EndTime - previous.StartTime;
                previous = current;
            }
        }
    }
}
