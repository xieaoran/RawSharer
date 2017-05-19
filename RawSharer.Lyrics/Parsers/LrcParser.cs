using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using RawSharer.Lyrics.Models;
using RawSharer.Lyrics.Utils;

namespace RawSharer.Lyrics.Parsers
{
    public static class LrcParser
    {
        private static readonly char[] OffsetNexts = { 'f', 'f', 's', 'e', 't' };
        /// <summary>
        /// Parse Lyrics from LRC-format Source Stream
        /// </summary>
        /// <param name="lrcStream">Source Stream</param>
        /// <returns>Parsed Lyrics</returns>
        public static Models.Lyrics Parse(Stream lrcStream)
        {
            var result = new Models.Lyrics();

            var lrcReader = new StreamReader(lrcStream);
            string line;
            while ((line = lrcReader.ReadLine()) != null)
            {
                ParseLine(result, line);
            }

            PostProcess(result);
            return result;
        }

        private static void PostProcess(Models.Lyrics lyrics)
        {
            lyrics.Sentences.Sort(new LyricsSentenceComparer());

            var hasOffset = lyrics.OffsetMs != 0;
            var offset = TimeSpan.FromMilliseconds(lyrics.OffsetMs);

            var previous = lyrics.Sentences[0];
            previous.Sequence = 0;
            if (hasOffset) previous.StartTime = previous.StartTime.Add(offset);
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

        private static void ParseLine(Models.Lyrics lyrics, string line)
        {
            var lineReader = new StringReader(line);

            if (lineReader.Read() != '[') return;
            var firstChar = (char)lineReader.Read();
            if (char.IsDigit(firstChar)) ParseSentence(lyrics, firstChar, lineReader);
            else ParseMetaData(lyrics, firstChar, lineReader);

            lineReader.Close();
        }

        private static void ParseSentence(Models.Lyrics lyrics, char firstChar, TextReader lineReader)
        {
            var timeBuilder = new StringBuilder();
            timeBuilder.Append(firstChar);

            var startTimes = new List<TimeSpan> { ParseTime(lineReader, timeBuilder) };
            int nextChar;
            while ((nextChar = lineReader.Read()) == '[')
            {
                startTimes.Add(ParseTime(lineReader, timeBuilder));
            }
            var value = nextChar == -1 ? string.Empty : (char) nextChar + lineReader.ReadToEnd().Trim();
            foreach (var startTime in startTimes)
            {
                lyrics.Sentences.Add(new LyricsSentence { StartTime = startTime, Value = value });
            }
        }
        private static void ParseMetaData(Models.Lyrics lyrics, char firstChar, TextReader lineReader)
        {
            MetaType metaType;
            if (firstChar == 'a')
            {
                var secondChar = lineReader.Read();
                if (secondChar == 'l') metaType = MetaType.Album;
                else if (secondChar == 'r') metaType = MetaType.Artist;
                else if (secondChar == 'u') metaType = MetaType.Author;
                else return;
            }
            else if (firstChar == 'b')
            {
                var secondChar = lineReader.Read();
                if (secondChar == 'y') metaType = MetaType.LrcCreator;
                else return;
            }
            else if (firstChar == 'o')
            {
                var nextFiveChars = new char[5];
                lineReader.Read(nextFiveChars, 0, 5);
                if (nextFiveChars.SequenceEqual(OffsetNexts)) metaType = MetaType.Offset;
                else return;
            }
            else if (firstChar == 't')
            {
                var secondChar = lineReader.Read();
                if (secondChar == 'i') metaType = MetaType.Title;
                else return;
            }
            else return;

            var nextChar = lineReader.Read();
            if (nextChar != ':') return;

            var value = lineReader.ReadToEnd().Trim();
            if (value[value.Length - 1] == ']') value = value.Substring(0, value.Length - 1);

            if (metaType == MetaType.Album) lyrics.Album = value;
            else if (metaType == MetaType.Artist) lyrics.Artist = value;
            else if (metaType == MetaType.Author) lyrics.Author = value;
            else if (metaType == MetaType.LrcCreator) lyrics.LrcCreator = value;
            else if (metaType == MetaType.Offset) lyrics.OffsetMs = int.Parse(value);
            else if (metaType == MetaType.Title) lyrics.Title = value;
        }

        private static TimeSpan ParseTime(TextReader lineReader, StringBuilder timeBuilder)
        {
            var flag = 0;
            int minute = 0, second = 0;
            char nextChar;
            while ((nextChar = (char)lineReader.Read()) != ']')
            {
                if (flag == 0 && nextChar == ':')
                {
                    minute = int.Parse(timeBuilder.ToString());
                    timeBuilder.Clear();
                    flag++;
                }
                else if (flag == 1 && (nextChar == '.' || nextChar == ':'))
                {
                    second = int.Parse(timeBuilder.ToString());
                    timeBuilder.Clear();
                    flag++;
                }
                else timeBuilder.Append(nextChar);
            }
            if (flag == 1)
            {
                second = int.Parse(timeBuilder.ToString());
                timeBuilder.Clear();
                return new TimeSpan(0, minute, second);
            }
            else
            {
                var millisecond = int.Parse(timeBuilder.ToString());
                for (var i = timeBuilder.Length; i < 3; i++)
                {
                    millisecond *= 10;
                }
                timeBuilder.Clear();
                return new TimeSpan(0, 0, minute, second, millisecond);
            }
        }
    }
}
