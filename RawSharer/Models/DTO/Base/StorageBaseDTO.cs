using System.Runtime.Serialization;
using Newtonsoft.Json;
using RawSharer.Models.Entities.Base;

namespace RawSharer.Models.DTO.Base
{
    public class StorageBaseDTO: EntityDTO
    {
        [JsonProperty(PropertyName = "storage_type")]
        [DataMember]
        public StorageType StorageType { get; protected set; }

        [JsonProperty(PropertyName = "content_type")]
        [DataMember]
        public string ContentType { get; protected set; }

        [JsonProperty(PropertyName = "md5_hash")]
        [DataMember]
        public string Md5Hash { get; protected set; }

        [JsonProperty(PropertyName = "length")]
        [DataMember]
        public long Length { get; protected set; }
    }
}