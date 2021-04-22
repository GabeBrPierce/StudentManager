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

        public Student GetStudentById(long id)
        {
            return Students.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Student> GetStudentByString(string text = null)
        {
            return from r in Students
                   where  String.IsNullOrEmpty(text) || r.Name.StartsWith(text) || r.Program.Name.StartsWith(text)
                   select r;
        }
        public Program NewProgram(Student student = null )
        {
            List<Student> tempStudents = new List<Student>();
            tempStudents.Add(student);
            int counter = 0;
            while (Programs.Where(x => x.Id == counter) != null)
            {
                counter++;
            }
            return new Program() { Id = counter, Name = $"program{counter}", Students = tempStudents };
        }
        public void EditStudent(long id, string name, long programId)
        {
            Student currentStudent = GetStudentById(id);
            if (currentStudent == null)
            {
                CreateStudent(id, name, programId);
            }
            List<Student> tempStudents = Students.ToList();
            int indexCurrentStudent = tempStudents.IndexOf(currentStudent);
            tempStudents.RemoveAt(indexCurrentStudent);
            Student newStudent;
            if (programId == currentStudent.Program.Id)
            {
                newStudent = new Student { Id = id, Name = name, Program = currentStudent.Program };
            }
            else if (Programs.Where(x => x.Id == programId) != null)
            {
                newStudent = new Student { Id = id, Name = name, Program = Programs.Where(x => x.Id == programId).FirstOrDefault() };
            }
            else
            {
                newStudent = new Student { Id = id, Name = name, Program = NewProgram(currentStudent) };
            }
            tempStudents.Insert(indexCurrentStudent, newStudent);
            Students = tempStudents;
        }

        public void CreateStudent(long id, string name, long programId)
        {
            Student newStudent = new Student();
            if (Students.Where(x => x.Id == id) == null)
            {
                newStudent.Id = id;
                newStudent.Name = name;
                if (Students.Where(x => x.Program.Id == programId) == null)
                {
                    newStudent.Program = NewProgram(newStudent);
                }
                else
                {
                    newStudent.Program = Programs.Where(x => x.Id == programId).FirstOrDefault();
                }
                List<Student> tempStudents = Students.ToList();
                tempStudents.Add(newStudent);
                Students = tempStudents;
            }
            else
            {
                EditStudent(id, name, programId);
            }
        }

        public void DeleteStudent(long id)
        {
            List<Student> tempStudents = Students.ToList();
            tempStudents.Remove(Students.Where(x => x.Id == id).FirstOrDefault());
            Students = tempStudents;
        }
    }
}
