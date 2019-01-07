using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Data.Entities;

namespace Web.Models
{
    public class QuestionModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Введите вопрос")]
        [Display(Name = "Вопрос")]
        public string Message { get; set; }

        [Required(ErrorMessage = "Выберите вариант правильного ответа")]
        public Guid SelectedAnswerId { get; set; }
        public List<AnswerModel> Answers { get; set; }

        public QuestionModel()
        {
            Answers = new List<AnswerModel>();
        }

        public QuestionModel(QuestionEntity question)
        {
            Id = question.Id;
            Message = question.Message;
            SelectedAnswerId = question.SelectedAnswerId;
            Answers = question.Answers.Select(x => new AnswerModel(x)).ToList();
        }
    }
}