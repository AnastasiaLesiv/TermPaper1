using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTermPaper
{
    public class Subject
    {

        public string subjectName;
        public string subjectDescription;
        public List<Group> groupsNames = new List<Group>();
        public List<Lesson> lessons = new List<Lesson>();
        public string SubjectName() => subjectName;
        public string SubjectDescription() => subjectDescription;
        public List<Group> GroupsNames() => groupsNames;
        public List<Lesson> Lessons() => lessons;
        public Subject(string subjectName, string subjectDescription, List<Group> groupsNames, List<Lesson> lessons)
        {
            this.subjectName = subjectName;
            this.subjectDescription = subjectDescription;
            this.groupsNames = groupsNames;
            this.lessons = lessons;
        }
        public uint GetGrade(string email)
        {
            uint result = 0;
            foreach (Lesson lesson in lessons)
            {
                foreach (Grade grade in lesson.Grade())
                {
                    if (grade.StudEmail() == email)
                    {
                        result += grade._Grade();
                    }
                }

            }
            return result;
        }
        public int GetAbsent(string email)
        {
            int result = 0;
            foreach (Lesson lesson in lessons)
            {
                foreach (string absent in lesson.Absent())
                {
                    if (absent == email)
                    {
                        result++;
                    }
                }

            }
            return result;
        }
    }
}
