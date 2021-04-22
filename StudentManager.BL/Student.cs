using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManager.BL
{
    public class Student
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Program Program { get; set; }
    }
}
