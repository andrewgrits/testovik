using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    [Table("Tests")]
    public class TestEntity : BaseEntity
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public virtual List<QuestionEntity> Questions { get; set; }
    }
}
