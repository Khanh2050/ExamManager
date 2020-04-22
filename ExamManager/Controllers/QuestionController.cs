using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamManager.DAL;
using ExamManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExamManager.Controllers
{
    public class QuestionController : Controller
    {
        private readonly ExamManagementContext context;

        public QuestionController(ExamManagementContext db)
        {
            context = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Question> questions = context.Questions;
            return View(questions);
        }

        public IActionResult CreateOrUpdate(int id)
        {
            if (id > 0)
            {
                Question question = context.Questions.Find(id);
                return View(question);
            }
            else
            {
                return View();
            }    
        }

        [HttpPost]
        public IActionResult CreateOrUpdate(Question model)
        {
            if (model.QuestionId > 0)
            {
                Question question = context.Questions.Find(model.QuestionId);
                question.Question1 = model.Question1;
                question.QuestionLevel = model.QuestionLevel;
                question.Answer = model.Answer;

                context.Questions.Update(question);
                context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                context.Questions.Add(model);
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }    
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            Question question = context.Questions.Find(id);
            context.Remove(question);
            context.SaveChanges();
            return Json(new { redirectUrl = Url.Action("Index", "Question") });
        }
    }
}