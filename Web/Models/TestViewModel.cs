using System;
using System.Collections.Generic;
using Data.Entities;

namespace Web.Models
{
    public class TestViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public List<QuestionModel> Questions { get; set; } = new List<QuestionModel>();
    }

    public class QuestionModel
    {
        public string Message { get; set; }
        public List<AnswerModel> Answers { get; set; } = new List<AnswerModel>();
    }

    public class AnswerModel
    {
        public string Message { get; set; }
        public bool IsTrue { get; set; }
    }
}
