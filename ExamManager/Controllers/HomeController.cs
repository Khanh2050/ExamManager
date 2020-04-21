using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ExamManager.Models;
using Microsoft.EntityFrameworkCore;
using ExamManager.DAL;

namespace ExamManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ExamManagementContext _db;

        public HomeController(ILogger<HomeController> logger, ExamManagementContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            //using (var db = new ExamManagementContext())
            //{

            //}
            var list = _db.Questions.ToList();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
