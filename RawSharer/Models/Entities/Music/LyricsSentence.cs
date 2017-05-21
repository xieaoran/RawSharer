using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RawSharer.Models.Entities.Base;

namespace RawSharer.Models.Entities.Music
{
    public class LyricsSentence : Entity
    {
        [Required]
        [Index(IsClustered = false, IsUnique = false)]
        public int Sequence { get; set; }
        [Required]
        public TimeSpan StartTime { get; set; }
        [Required]
        public TimeSpan EndTime { get; set; }
        [Required]
        public TimeSpan Duration { get; set; }
        public string Value { get; set; }

        public virtual Lyrics Lyrics { get; set; }

        public LyricsSentence(int sequence,
            TimeSpan startTime, TimeSpan endTime, TimeSpan duration,
            string value)
        {
            Id = Guid.NewGuid();
            Sequence = sequence;
            StartTime = startTime;
            EndTime = endTime;
            Duration = duration;
            Value = value;
        }

        public LyricsSentence()
        {
            // Reserved for Serialization
        }
    }
}