using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using RawSharer.Models.DTO.Base;

namespace RawSharer.Models.DTO.Music.Reverse
{
    public class TrackDTOReverse : EntityDTO
    {
        [JsonProperty(PropertyName = "name")]
        [DataMember]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "disk_number")]
        [DataMember]
        public byte? DiskNumber { get; set; }

        [JsonProperty(PropertyName = "track_number")]
        [DataMember]
        public byte? TrackNumber { get; set; }

        [JsonProperty(PropertyName = "duration")]
        [DataMember]
        public TimeSpan? Duration { get; set; }

    }
}