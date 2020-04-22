using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ExamManager.DAL;
using ExamManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExamManager.Controllers
{
    public class ExamController : Controller
    {
        private readonly ExamManagementContext _db;

        public ExamController(ExamManagementContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            ViewBag.QuestionsCount = CountQuestionPerLevel();
            return View();
        }

        public IActionResult ExamSetupModal()
        {
            return PartialView();
        }

        [HttpPost]
        public IActionResult Content(ExamModel model)
        {
            var questionList = new List<Question>();
            int currentLvl = 1;
            foreach (PropertyInfo lvl in model.GetType().GetProperties())
            {              
                var questions = (int)lvl.GetValue(model, null);
                var list = GetRandomQuestion(currentLvl, questions);
                questionList.AddRange(list);
                currentLvl++;
            }
            return PartialView(questionList);
        }

        private IEnumerable<Question> GetRandomQuestion(int lvl, int quantity)
        {
            try
            {
                Random random = new Random();
                var list = _db.Questions.Where(x => x.QuestionLevel == lvl);
                int seed = random.Next(list.Count());

                //var z = list.OrderBy(s => Guid.NewGuid())
                //         .Take(quantity);

                return list.OrderBy(s => (s.QuestionLevel & seed) & (s.QuestionLevel | seed))
                           .Take(quantity);
            }
            catch(Exception ex)
            {
                return new List<Question>();
            }
        }

        private ExamModel CountQuestionPerLevel()
        {
            var list = _db.Questions.OrderBy(x => x.QuestionLevel)
                                    .GroupBy(p => p.QuestionLevel,
                                            (p, g) => new QuestionModel
                                            {
                                                Level = p,
                                                Count = g.Count()
                                            }).AsEnumerable();

            var model = new ExamModel
            {
                Lvl1 = list.Where(x => x.Level == 1).DefaultIfEmpty(new QuestionModel() { Count = 0 }).First().Count,
                Lvl2 = list.Where(x => x.Level == 2).DefaultIfEmpty(new QuestionModel() { Count = 0 }).First().Count,
                Lvl3 = list.Where(x => x.Level == 3).DefaultIfEmpty(new QuestionModel() { Count = 0 }).First().Count,
                Lvl4 = list.Where(x => x.Level == 4).DefaultIfEmpty(new QuestionModel() { Count = 0 }).First().Count,
                Lvl5 = list.Where(x => x.Level == 5).DefaultIfEmpty(new QuestionModel() { Count = 0 }).First().Count
            };
            return model;
        }
    }
}