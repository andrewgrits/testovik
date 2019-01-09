using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.Language.Extensions;
using Web.Models;

namespace Web.Misc
{
    public static class ModelGenerator
    {
        public static TestModel GenerateTestModel()
        {
            var test = new TestModel();
            test.Questions.AddRange(GenerateQuestionModels(2, 2));
            test.Questions.AddRange(GenerateQuestionModels(4, 3));
            test.Questions.AddRange(GenerateQuestionModels(4, 4));

            return test;
        }

        private static IEnumerable<QuestionModel> GenerateQuestionModels(int questionsCount, int answersCount)
        {
            if (questionsCount <= 0 || answersCount <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            var questions = new List<QuestionModel>();

            for (int i = 0; i < questionsCount; i++)
            {
                questions.Add(new QuestionModel
                {
                    Answers = GenerateAnswerModels(answersCount).ToList()
                });
            }

            return questions;
        }

        private static IEnumerable<AnswerModel> GenerateAnswerModels(int answersCount)
        {
            if (answersCount <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            var answers = new List<AnswerModel>();

            for (int i = 0; i < answersCount; i++)
            {
                answers.Add(new AnswerModel
                {
                    Id = Guid.NewGuid()
                });
            }

            return answers;
        }
    }
}
