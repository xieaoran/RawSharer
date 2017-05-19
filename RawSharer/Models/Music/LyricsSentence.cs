using System;
using System.ComponentModel.DataAnnotations;
using RawSharer.Models.BaseClasses;

namespace RawSharer.Models.Music
{
    public class LyricsSentence : Entity
    {
        [Required]
        public int Sequence { get; set; }
        [Required]
        public TimeSpan StartTime { get; set; }
        [Required]
        public TimeSpan EndTime { get; set; }
        [Required]
        public TimeSpan Duration { get; set; }
        [Required]
        public string Content { get; set; }

        public virtual Lyrics Lyrics { get; set; }

        public LyricsSentence(int sequence,
            TimeSpan startTime, TimeSpan endTime,
            string content)
        {
            Id = Guid.NewGuid();
            Sequence = sequence;
            StartTime = startTime;
            EndTime = endTime;
            Duration = EndTime - StartTime;
            Content = content;
        }
    }
}