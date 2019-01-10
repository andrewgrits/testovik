using System;
using System.Linq;
using System.Threading.Tasks;
using Data.Context.Repository;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Misc;
using Web.Models;

namespace Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminPanelController : Controller
    {
        private readonly IRepository<TestEntity> testRepository;
        private readonly IHostingEnvironment appEnvironment;

        public AdminPanelController(IRepository<TestEntity> testRepository,
            IHostingEnvironment appEnvironment)
        {
            this.testRepository = testRepository;
            this.appEnvironment = appEnvironment;
        }

        public IActionResult Tests()
        {
            var tests = testRepository
                .Where(x => x.IsActive).ToList()
                .Select(x => new TestModel(x)).ToList();

            return View(tests);
        }

        public IActionResult Create()
        {
            return RedirectToAction("Edit");
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id != null)
            {
                var test = await testRepository.FirstOrDefaultAsync(x => x.Id == id);
                return View(new TestModel(test));
            }

            return View(ModelGenerator.GenerateTestModel());
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TestModel testModel, IFormFile image)
        {
            if (testModel.Id == Guid.Empty)
            {
                await CreateTest(testModel, image);
                return RedirectToAction("Tests");
            }

            var test = await testRepository.FirstOrDefaultAsync(x => x.Id == testModel.Id);

            if (test == null)
            {
                return View(testModel);
            }

            await EditExistingTest(test, testModel, image);
            await testRepository.SaveContextAsync();
            TempData["Success"] = $"\"{test.Name}\" успешно сохранён";

            return RedirectToAction("Tests");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var test = await testRepository.FirstOrDefaultAsync(x => x.Id == id);

            if (test == null)
            {
                return RedirectToAction("Tests");
            }

            test.IsActive = false;
            await testRepository.SaveContextAsync();

            return RedirectToAction("Tests");
        }



        private async Task CreateTest(TestModel testModel, IFormFile image)
        {
            var test = new TestEntity
            {
                Name = testModel.Name,
                IsActive = true,
                Questions = testModel.Questions.Select(question => new QuestionEntity
                {
                    Message = question.Message,
                    SelectedAnswerId = question.SelectedAnswerId,
                    Answers = question.Answers.Select(answer => new AnswerEntity
                    {
                        Id = answer.Id,
                        Message = answer.Message
                    }).ToList()
                }).ToList()
            };

            if (image != null)
            {
                test.ImagePath = await FileUploader.UploadImage(image, appEnvironment);
            }

            await testRepository.CreateAsync(test);
            TempData["Success"] = $"\"{test.Name}\" успешно сохранён";
        }

        private async Task EditExistingTest(TestEntity existingTest, TestModel test, IFormFile image)
        {
            existingTest.Name = test.Name;

            if (image != null)
            {
                existingTest.ImagePath = await FileUploader.UploadImage(image, appEnvironment);
            }

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
