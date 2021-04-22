using System.Collections.Generic;

namespace StudentManager.BL
{
    public interface ISchool
    {
        IEnumerable<Program> Programs { get; set; }
        IEnumerable<Student> Students { get; set; }
        Student GetStudentById(long id);
        IEnumerable<Student> GetStudentByString(string text);
        void CreateStudent(long id, string name, long programId);
        void EditStudent(long id, string name, long programId);
        void DeleteStudent(long id);
    }
}