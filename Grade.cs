using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTermPaper
{
    public class Grade

    {
        public string studEmail;
        public uint grade;
        public string StudEmail() => studEmail;
        public uint _Grade() => grade;
        public Grade(string studEmail, uint grade)
        {
            this.studEmail = studEmail;
            this.grade = grade;
        }
    }
}
