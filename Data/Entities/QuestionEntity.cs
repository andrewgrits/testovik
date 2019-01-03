using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    [Table("Questions")]
    public class QuestionEntity : BaseEntity
    {
        public string Message { get; set; }
        public virtual TestEntity Test { get; set; }
        public virtual List<AnswerEntity> Answers { get; set; }
    }
}
