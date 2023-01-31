using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTermPaper
{
    public class Student
    {
        public string nameSurname;
        public string email;
        public bool elder;
        public string NameSurname() => nameSurname;
        public string Email() => email;
        public bool Elder() => elder;
        public Student(string email, string name, bool elder)
        {
            this.email = email;
            this.nameSurname = name;
            this.elder = elder;
        }
    }
}
