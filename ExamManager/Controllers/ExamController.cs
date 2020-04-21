using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ExamManager.Controllers
{
    public class ExamController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ExamSetupModal()
        {
            return PartialView();
        }

        public IActionResult Content()
        {
            return PartialView();
        }
        //public IActionResult ShowSpinner()
        //{
        //    return PartialView("_Spinner");
        //}
    }
}