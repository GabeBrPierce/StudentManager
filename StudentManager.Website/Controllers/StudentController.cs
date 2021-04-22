using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudentManager.BL;
using StudentManager.Website.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManager.Website.Controllers
{
    public class StudentController : Controller
    {
        private readonly ISchool school;
        
        
        public StudentController(ISchool school)
        {
            this.school = school;
        }
        [HttpGet]
        public IActionResult Index(string searchTerm)
        {
            return View(school.GetStudentByString(searchTerm));
        }
        
        public IActionResult Create()
        {
            return View(school);
        }
        public IActionResult Delete()
        {
            return View(school);
        }
        public IActionResult Edit()
        {
            return View(school);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
