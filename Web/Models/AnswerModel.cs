using System;
using System.ComponentModel.DataAnnotations;
using Data.Entities;

namespace Web.Models
{
    public class AnswerModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Введите ответ")]
        [Display(Name = "Вариант ответа")]
        public string Message { get; set; }

        public AnswerModel()
        { }

        public AnswerModel(AnswerEntity answer)
        {
            Id = answer.Id;
            Message = answer.Message;
        }
    }
}