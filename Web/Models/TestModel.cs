using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Data.Entities;

namespace Web.Models
{
    public class TestModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Введите название теста")]
        [Display(Name = "Название")]
        public string Name { get; set; }

        public string ImagePath { get; set; }

        public List<QuestionModel> Questions { get; set; }

        public TestModel()
        {
            Questions = new List<QuestionModel>();
        }

        public TestModel(TestEntity test)
        {
            Id = test.Id;
            Name = test.Name;
            ImagePath = test.ImagePath;
            Questions = test.Questions.Select(x => new QuestionModel(x)).ToList();
        }
    }
}