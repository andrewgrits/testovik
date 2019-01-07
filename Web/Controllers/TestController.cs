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
        private readonly IRepository<TestEntity> testRepository;

        public TestController(IRepository<TestEntity> testRepository)
        {
            this.testRepository = testRepository;
        }

        public async Task<IActionResult> Pass(Guid testId)
        {
            var test = await testRepository.FirstOrDefaultAsync(x => x.Id == testId);

            return View(GetTestModelWithoutSelectedAnswers(new TestModel(test)));
        }

        [HttpPost]
        public async Task<IActionResult> Pass(TestModel testModel)
        {
            var test = await testRepository.FirstOrDefaultAsync(x => x.Id == testModel.Id);

            if (test == null || testModel.Questions.Count < test.Questions.Count)
            {
                return Redirect($"~/Test/Pass?testId={testModel.Id}");
            }

            var rightAnswersCount = 0;
            for (int i = 0; i < testModel.Questions.Count; i++)
            {
                if (testModel.Questions[i].SelectedAnswerId == test.Questions[i].SelectedAnswerId)
                {
                    rightAnswersCount++;
                }
            }

            return RedirectToAction("ShowResult", new ResultModel
            {
                AnswersCount = testModel.Questions.Count,
                RightAnswersCount = rightAnswersCount
            });
        }

        public IActionResult ShowResult(ResultModel result)
        {
            return View(result);
        }

        private TestModel GetTestModelWithoutSelectedAnswers(TestModel test)
        {
            foreach (var question in test.Questions)
            {
                question.SelectedAnswerId = Guid.Empty;
            }

            return test;
        }
    }
}