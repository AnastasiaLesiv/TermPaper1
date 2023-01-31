using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTermPaper
{
    public class Group
    {
        public string groupName;
        public string description;
        public List<Student> students;
        public string GroupName() => groupName;
        public string Description() => description;
        public List<Student> Students() => students;
        public Group(string groupName, string description, List<Student> students)
        {
            this.groupName = groupName;
            this.description = description;
            this.students = students;
        }
    }
}
