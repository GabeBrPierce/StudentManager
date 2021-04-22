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
        [HttpPost]
        public IActionResult Create(long id, string name, long programId)
        {
            school.CreateStudent(id, name, programId);
            return View("Index", school.GetStudentByString(null));
        }
        [HttpPost]
        public IActionResult Delete(long id)
        {
            school.DeleteStudent(id);
            return View("Index", school.GetStudentByString(null));
        }
        [HttpPost]
        public IActionResult Edit(long id, string name, long programId)
        {
            school.EditStudent(id, name, programId);
            return View("Index", school.GetStudentByString(null));
        }
        [HttpGet]
        public IActionResult Edit(long id)
        {
            return View(school.GetStudentById(id));
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
