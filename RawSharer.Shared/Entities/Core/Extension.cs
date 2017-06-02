using System.ComponentModel.DataAnnotations;

namespace RawSharer.Shared.Entities.Core
{
    public class Extension : EntityBase
    {
        [MaxLength(128)]
        public string Name { get; set; }

        [Required]
        [MaxLength(32)]
        public string Contract { get; set; }

        [Required]
        [MaxLength(256)]
        public string EntryPoint { get; set; }
    }
}
