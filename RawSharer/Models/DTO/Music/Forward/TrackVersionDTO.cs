using System.Runtime.Serialization;
using Newtonsoft.Json;
using RawSharer.Models.DTO.Base;
using RawSharer.Models.DTO.Music.Reverse;

namespace RawSharer.Models.DTO.Music.Forward
{
    public class TrackVersionDTO : EntityDTO
    {
        [JsonProperty(PropertyName = "name")]
        [DataMember]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "track")]
        [DataMember]
        public TrackDTOReverse Track { get; set; }
    }
}