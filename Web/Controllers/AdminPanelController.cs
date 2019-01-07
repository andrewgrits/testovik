using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Context.Repository;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Controllers
{
    public class AdminPanelController : Controller
    {
        private readonly IRepository<TestEntity> testRepository;
        private readonly IRepository<QuestionEntity> questionRepository;
        private readonly IRepository<AnswerEntity> answerRepository;

        public AdminPanelController(
            IRepository<TestEntity> testRepository,
            IRepository<QuestionEntity> questionRepository,
            IRepository<AnswerEntity> answerRepository)
        {
            this.testRepository = testRepository;
            this.questionRepository = questionRepository;
            this.answerRepository = answerRepository;
        }

        public IActionResult Tests()
        {
            var tests = testRepository.ToList()
                .Select(x => new TestModel(x)).ToList();

            return View(tests);
        }

        public IActionResult Create()
        {
            return View("Edit");
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var test = await testRepository.FirstOrDefaultAsync(x => x.Id == id);
            return View(new TestModel(test));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TestModel testModel)
        {
            var test = await testRepository.FirstOrDefaultAsync(x => x.Id == testModel.Id);

            if (test == null)
            {
                return View(testModel);
            }

            EditExistingTest(test, testModel);
            await testRepository.SaveContextAsync();
            TempData["Success"] = $"\"{test.Name}\" успешно сохранён";

            return RedirectToAction("Tests");
        }

        private void EditExistingTest(TestEntity existingTest, TestModel test)
        {
            existingTest.Name = test.Name;
            // TODO: Add new image

            foreach (var question in test.Questions)
            {
                var existingQuestion = FindQuestionById(question.Id, existingTest);
                if (existingQuestion == null)
                {
                    continue;
                }

                EditExistingQuestion(existingQuestion, question);
            }
        }

        private QuestionEntity FindQuestionById(Guid questionId, TestEntity test)
        {
            return test.Questions.FirstOrDefault(x => x.Id == questionId);
        }

        private void EditExistingQuestion(QuestionEntity existingQuestion, QuestionModel changedQuestion)
        {
            existingQuestion.Message = changedQuestion.Message;
            existingQuestion.SelectedAnswerId = changedQuestion.SelectedAnswerId;

            foreach (var answer in changedQuestion.Answers)
            {
                var existingAnswer = FindAnswerById(answer.Id, existingQuestion);
                if (existingAnswer == null)
                {
                    continue;
                }

                EditExistingAnswer(existingAnswer, answer);
            }
        }

        private AnswerEntity FindAnswerById(Guid answerId, QuestionEntity question)
        {
            return question.Answers.FirstOrDefault(x => x.Id == answerId);
        }

        private void EditExistingAnswer(AnswerEntity existingAnswer, AnswerModel changedAnswer)
        {
            existingAnswer.Message = changedAnswer.Message;
        }
    }
}
