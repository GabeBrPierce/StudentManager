using System.Collections.Generic;

namespace StudentManager.BL
{
    public interface ISchool
    {
        IEnumerable<Program> Programs { get; set; }
        IEnumerable<Student> Students { get; set; }
        Student GetStudentById(int id);
        IEnumerable<Student> GetStudentByString(string text);
    }
}