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

        public async Task<IActionResult> Pass()
        {
            var test = await testRepository.FirstOrDefaultAsync();
            var testModel = new TestViewModel
            {
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
    }
}