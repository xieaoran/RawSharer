using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RawSharer.Models.BaseClasses
{
    public abstract class Entity
    {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; protected set; }

        public DateTime? TimeStamp { get; protected set; }
    }
}