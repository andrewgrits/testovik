using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    [Table("Answers")]
    public class AnswerEntity : BaseEntity
    {
        public string Message { get; set; }
        public bool IsTrue { get; set; }
        public virtual QuestionEntity Question { get; set; }
    }
}
