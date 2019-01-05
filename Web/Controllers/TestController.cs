using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Context;
using Data.Context.Repository;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Controllers
{
    public class TestController : Controller
    {
        private readonly IRepository<TestEntity, BaseDbContext> testRepository;

        public TestController(IRepository<TestEntity, BaseDbContext> testRepository)
        {
            this.testRepository = testRepository;
        }

        public async Task<IActionResult> Pass(Guid testId)
        {
            var test = await testRepository.FirstOrDefaultAsync(x => x.Id == testId);
            var testModel = new TestViewModel
            {
                Id = test.Id,
                Name = test.Name,
                ImagePath = test.ImagePath,
                Questions = test.Questions.Select(question => new QuestionModel
                {
                    Message = question.Message,
                    Answers = question.Answers.Select(answer => new AnswerModel
                    {
                        Message = answer.Message
                    }).ToList()
                }).ToList()
            };

            return View(testModel);
        }

        [HttpPost]
        public async Task<IActionResult> Pass(TestViewModel testViewModel)
        {
            var test = await testRepository.FirstOrDefaultAsync(x => x.Id == testViewModel.Id);

            if (test == null || testViewModel.Questions.Count < test.Questions.Count)
            {
                return Redirect($"~/Test/Pass?testId={testViewModel.Id}");
            }

            var rightAnswersCount = 0;
            for (int i = 0; i < testViewModel.Questions.Count; i++)
            {
                var rightAnswer = test.Questions[i].Answers.FirstOrDefault(x => x.IsTrue);

                if (testViewModel.Questions[i].Message == rightAnswer.Message)
                {
                    rightAnswersCount++;
                }
            }

            return Ok(rightAnswersCount);
        }
    }
}