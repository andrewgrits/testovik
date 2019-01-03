using System.Collections.Generic;
using Data.Entities;

namespace Web.Models
{
    public class TestViewModel
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public List<QuestionModel> Questions { get; set; }
    }

    public class QuestionModel
    {
        public string Message { get; set; }
        public List<AnswerModel> Answers { get; set; }
    }

    public class AnswerModel
    {
        public string Message { get; set; }
        public bool IsTrue { get; set; }
    }
}
