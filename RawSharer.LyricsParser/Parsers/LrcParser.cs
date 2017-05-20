using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using RawSharer.LyricsParser.Models;
using RawSharer.LyricsParser.Utils;

namespace RawSharer.LyricsParser.Parsers
{
    public static class LrcParser
    {
        private static readonly char[] OffsetNexts = { 'f', 'f', 's', 'e', 't' };

        /// <summary>
        /// Parse Lyrics from LRC-format Lyrics String
        /// </summary>
        /// <param name="lrcString">Lyrics String</param>
        /// <returns>Parsed Lyrics</returns>
        public static ParsedLyrics Parse(string lrcString)
        {
            try
            {
                var result = new ParsedLyrics();

                var reader = new StringReader(lrcString);
                while (ParseLine(result, reader)) { }
                reader.Close();

                PostProcess(result);
                return result;
            }
            catch (Exception exception)
            {
                throw new ParseException(Resources.ExceptionMessages.GenericFailure, exception);
            }
        }

        /// <summary>
        /// Parse Lyrics from LRC-format Lyrics Source Stream
        /// </summary>
        /// <param name="lrcStream">Lyrics Source Stream</param>
        /// <returns>Parsed Lyrics</returns>
        public static ParsedLyrics Parse(Stream lrcStream)
        {
            try
            {
                var result = new ParsedLyrics();

                var reader = new StreamReader(lrcStream);
                while (ParseLine(result, reader)) { }
                reader.Close();

                PostProcess(result);
                return result;
            }
            catch (Exception exception)
            {
                throw new ParseException(Resources.ExceptionMessages.GenericFailure, exception);
            }
        }

        private static void PostProcess(ParsedLyrics lyrics)
        {
            lyrics.Sentences.Sort(new LyricsSentenceComparer());

            var hasOffset = lyrics.OffsetMs != 0;
            var offset = TimeSpan.FromMilliseconds(lyrics.OffsetMs);

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

        private static bool ParseLine(ParsedLyrics lyrics, TextReader reader)
        {
            var leftBracket = reader.Read();
            if (leftBracket == -1) return false;
            if (leftBracket != '[')
            {
                reader.ReadLine();
                return true;
            }
            var firstChar = (char)reader.Read();
            if (char.IsDigit(firstChar)) ParseSentence(lyrics, reader, firstChar);
            else ParseMetaData(lyrics, reader, firstChar);
            return true;
        }

        private static void ParseSentence(ParsedLyrics lyrics, TextReader reader, char firstChar)
        {
            var timeBuilder = new StringBuilder();
            timeBuilder.Append(firstChar);

            var startTimes = new List<TimeSpan> { ParseTime(reader, timeBuilder) };
            int nextChar;
            while ((nextChar = reader.Read()) == '[')
            {
                startTimes.Add(ParseTime(reader, timeBuilder));
            }
            var value = nextChar == '\n' || nextChar == -1 ?
                string.Empty : (char)nextChar + reader.ReadLine()?.Trim();
            foreach (var startTime in startTimes)
            {
                lyrics.Sentences.Add(new ParsedSentence { StartTime = startTime, Value = value });
            }
        }
        private static void ParseMetaData(ParsedLyrics lyrics, TextReader reader, char firstChar)
        {
            MetaType metaType;
            if (firstChar == 'a')
            {
                var secondChar = reader.Read();
                if (secondChar == 'l') metaType = MetaType.Album;
                else if (secondChar == 'r') metaType = MetaType.Artist;
                else if (secondChar == 'u') metaType = MetaType.Author;
                else return;
            }
            else if (firstChar == 'b')
            {
                var secondChar = reader.Read();
                if (secondChar == 'y') metaType = MetaType.LrcCreator;
                else return;
            }
            else if (firstChar == 'o')
            {
                var nextFiveChars = new char[5];
                reader.Read(nextFiveChars, 0, 5);
                if (nextFiveChars.SequenceEqual(OffsetNexts)) metaType = MetaType.Offset;
                else return;
            }
            else if (firstChar == 't')
            {
                var secondChar = reader.Read();
                if (secondChar == 'i') metaType = MetaType.Title;
                else return;
            }
            else return;

            var nextChar = reader.Read();
            if (nextChar != ':') return;

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
        }

        private static TimeSpan ParseTime(TextReader reader, StringBuilder timeBuilder)
        {
            var flag = 0;
            int minute = 0, second = 0;
            char nextChar;
            while ((nextChar = (char)reader.Read()) != ']')
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
