using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace RawSharer.Models.DTO.Base
{
    public class EntityDTO
    {
        [JsonProperty(PropertyName = "id")]
        [DataMember]
        public Guid Id { get; protected set; }

        [JsonProperty(PropertyName = "timestamp")]
        [DataMember]
        public DateTime? TimeStamp { get; protected set; }
    }
}