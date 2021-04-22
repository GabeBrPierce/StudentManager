using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManager.BL
{
    public class School : ISchool
    {
        public IEnumerable<Student> Students { get; set; }
        public IEnumerable<Program> Programs { get; set; }
        public School()
        {
            List<Student> tempStudents = new List<Student>();
            List<Program> tempProgram = new List<Program>();
            // I know this isn't the traditional way of doing things, and I know that overall this is more work than 
            // what I could have done, but this way I can make thousands of students and thousands of programs
            // very easily. So there you have it.
            int studentCount = 50;
            int programCount = 10;
            
            // Making Ienum of students with no program value so I can populate them later
            for (long i = 1; i <= studentCount; i++)
            {
                tempStudents.Add(new Student { Name = $"student{i}", Id = i, Program = new Program { } });
            }
            // Making Ienum of programs with no ienum of students so I can populate them later
            for (long i = 1; i <= programCount; i++)
            {
                tempProgram.Add(new Program { Name = $"program{i}", Id = i, Students = new List<Student>() });
            }
            // Populating both 
            
            foreach (var student in tempStudents)
            {
                float _id = student.Id;
                string prgName = $"program{_id % 10 + 1}";
                Program program = tempProgram.Where(x => x.Name == prgName).FirstOrDefault();
                program.Students.Add(student);
                student.Program = program;
            }
            Students = tempStudents;
            Programs = tempProgram;
        }

        public Student GetStudentById(int id)
        {
            return Students.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Student> GetStudentByString(string text = null)
        {
            return from r in Students
                   where  String.IsNullOrEmpty(text) || r.Name.StartsWith(text) || r.Program.Name.StartsWith(text)
                   select r;
        }
    }
}
