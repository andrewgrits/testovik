using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
    }
}
