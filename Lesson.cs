using MyTermPaper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTermPaper
{
    public class Lesson
    {
        public DateTime date;
        public string type;
        public List<string> absent = new List<string>();
        public List<Grade> grade = new List<Grade>();
        public DateTime Date() => date;
        public string Type() => type;
        public List<string> Absent() => absent;
        public List<Grade> Grade() => grade;

        public Lesson(DateTime date, string type, List<string> email, List<Grade> grade)
        {
            this.date = date;
            this.type = type;
            this.absent = email;
            this.grade = grade;
        }

    }
}
