using System.Text;
using Newtonsoft.Json;
using System.IO;

namespace MyTermPaper
{
    class Program
    {

        static int GroupSubject(List<Group> groups , List<Subject> subjects)
        {
            Console.Clear();
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
            Console.WriteLine(new String('_', 40));
            Console.WriteLine("| Групи  " + new String(' ', 30));
            if (groups != null)
            {
                foreach (var item in groups)
                {
                    Console.WriteLine($"| {item.GroupName(),5} ({item.Description(),10}) ");
                }
            }

            Console.WriteLine(new String('-', 40));
            Console.WriteLine("| Предмети  " + new String(' ', 27));
            if (subjects != null)
            {
                foreach (var item in subjects)
                {
                    Console.WriteLine($"| {item.SubjectName()} ({item.SubjectDescription(),10}) ");
                }
            }

            Console.WriteLine(new String('_', 40));
            Console.WriteLine();
            Console.WriteLine(
                "Опцiї:\n1 - Додати нову групу\n2 - Додати новий предмет\n3 - Обрати групу, щоб переглянути iнформацiю про неї\n4 - Обрати предмет, щоб переглянути інформацію про нього\n5 - Зберегти всі зміни");
            Console.Write("Вибір: ");
            int choice = int.Parse(Console.ReadLine());
            Console.Clear();
            return choice;
        }

        static int counter;

        static Group AddGroup()
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
            Console.Write("Введiть назву групи (з великої лiтери):  ");
            string nameGroup = Console.ReadLine();
            Console.Write("\nВведiть опис групи:  ");
            string descriptionGroup = Console.ReadLine();
            Console.WriteLine(
                "\nВведiть по черзi всiх студентiв групи\nДля продовження натискайте \"Enter\", для завершення натисніть \"Escape\"");
            List<Student> students = new List<Student>();

            while (true)
            {
                ConsoleKeyInfo input = Console.ReadKey();

                if (input.Key != ConsoleKey.Escape)
                {
                    Console.WriteLine();
                    students.Add(AddStudent());
                }
                else
                {
                    break;
                }
            }

            Group group = new Group(nameGroup, descriptionGroup, students);
            counter--;
            Console.Clear();
            return group;
        }

        static Student AddStudent()
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
            Console.Write("Введiть iм'я та прiзвище студента:  ");
            string nameStudent = Console.ReadLine();
            Console.Write("Введiть академiчну електронну пошту студента (@vu.cdu.edu.ua):  ");
            string emailStudent;
            while (true)
            {
                emailStudent = Console.ReadLine();
                if (emailStudent.Contains("@vu.cdu.edu.ua"))
                    break;
                Console.Write("Введiть саме корпоративну пошту:  ");
            }

            Console.Write("Якщо студент є старостою, натиснiть \"1\", якщо нi - \"0\":  ");
            int el;
            bool elder = false;

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out el))
                {
                    while (el == 1)
                    {
                        if (counter == 0)
                        {
                            nameStudent += " (староста)";
                            elder = true;
                            ++counter;
                            break;
                        }

                        if (counter > 0 && el == 1)
                        {
                            Console.WriteLine("У групi не може бути 2 старости.");
                            break;
                        }

                        break;
                    }

                    break;
                }
                else
                {
                    Console.WriteLine("Не коректно введене значення!");
                }
            }

            var student = new Student(emailStudent, nameStudent, elder);
            return student;
        }

        static Subject AddSubject(List<Group> groups)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
            Console.Write("Введiть назву предмету: ");
            string nameSubject = Console.ReadLine();
            Console.Write("\nВведiть опис предмету: ");
            string descriptionSubject = Console.ReadLine();
            Console.WriteLine("Групи:");
            List<Group> groupName = new List<Group>();
            for (int i = 0; i < groups.Count; i++)
            {
                Console.WriteLine($"{groups[i].GroupName()}");
            }
            Console.WriteLine("\nВведiть по черзi всi назви груп, якi його вiдвiдують (з великої лiтери)\nДля завершення введiть \"Стоп\"");
            Console.WriteLine();
            Console.WriteLine("Обрані групи:");
            while (true)
            {
                string message = Console.ReadLine();
                if (message.ToLower() != "стоп")
                {
                    for (int i = 0; i < groups.Count; i++)
                    {
                        if (message == groups[i].GroupName())
                        {
                            groupName.Add(groups[i]);
                            break;
                        }
                        else if (i == groups.Count - 1)
                        {
                            Console.WriteLine("Нажаль, такої групи не iснує, спробуйте ще раз.");
                        }
                    }
                }
                else
                {
                    break;
                }
            }

            List<Lesson> lessons = new List<Lesson>();
            Console.WriteLine("\nВведiть по черзi всi пари, якi вiдведенi на цей предмет\nДля продовження натиснiть \"Enter\"\nДля завершення натисніть \"Escape\"");
            while (true)
            {
                ConsoleKeyInfo input = Console.ReadKey();
                if (input.Key != ConsoleKey.Escape)
                {
                    Console.WriteLine("\nНова пара");
                    lessons.Add(AddLesson(groupName));
                }
                else
                {
                    break;
                }
            }

            var subject = new Subject(nameSubject, descriptionSubject, groupName, lessons);
            Console.Clear();
            return subject;
        }

        int count;

        static Lesson AddLesson(List<Group> groupName)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
            Console.Write("Введiть дату пари: ");
            DateTime date;
            while (DateTime.TryParse(Console.ReadLine(), out date) != true)
            {
                Console.WriteLine("Введiть дата в форматi \"00.00.0000\"");
            }
            string type;
            while (true)
            {
                Console.Write("Введiть тип пари (лекцiя, практична, лабораторна): ");
                type = Console.ReadLine();
                if (type == "лекція" || type == "практична" || type == "лабораторна")
                {
                    break;
                }

                Console.WriteLine("Некоректне введення. Введiть один iз наданих типiв пари.");
            }

            List<string> absent = new List<string>();
            List<Grade> scores = new List<Grade>();
            Console.WriteLine("\nВведiть по черзi результати роботи кожного студента вiдповiдно пари: \nЯкщо був вiдсутнiй - (-1)\nЯкщо просто був присутнiй - 0\nЯкщо отримав оцiнку вiд 1-5 ввести оцiнку ");

            for (int i = 0; i < groupName.Count; i++)
            {
                foreach (var item in groupName[i].Students())
                {
                    while (true)
                    {
                        Console.Write($"{item.NameSurname()} : ");
                        string result = Console.ReadLine();
                        if (result == "-1")
                        {
                            absent.Add(item.Email());
                            break;
                        }
                        else if (result == "0" || result == "1" || result == "2" || result == "3" || result == "4" ||
                                 result == "5")
                        {
                            scores.Add(new Grade(item.Email(), uint.Parse(result)));
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Невiрно введене значення, спробуйте ще раз.");
                        }
                    }
                }
            }

            Lesson lesson = new Lesson(date, type, absent, scores);
            return lesson;
        }

        static void GroupSelection(List<Group> groups, List<Subject> subjects)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
            if (groups.Count == 0)
            {
                Console.WriteLine("Ще жодної групи не створено.");
                Console.WriteLine("Натиснiть \"Enter\", щоб повернутись назад.");
                Console.ReadKey();
                return;
            }
            Console.WriteLine("Введiть число, пiд яким знаходиться обрана вами група, щоб переглянути iнформацiю про неї.");
            for (int i = 0; i < groups.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {groups[i].GroupName()}");
            }

            Console.Write("\nВибір: ");
            int choice = int.Parse(Console.ReadLine());
            while (true)
            {
                if (choice > 0 && choice <= groups.Count)
                {
                    break;
                }

                Console.WriteLine("Неправильно. Введiть один iз наданих номерiв груп.");
            }
            Console.WriteLine();
            for (int i = 0; i < groups.Count; i++)
            {
                if (i == choice - 1)
                {
                    Console.WriteLine(new String('-', 30));
                    Console.WriteLine($"{groups[i].GroupName()} ({groups[i].Description()})");
                    Console.WriteLine(new String('-', 30));
                    foreach (var item in groups[i].Students())
                    {
                        Console.WriteLine(item.NameSurname());
                    }

                    Console.WriteLine(new String('-', 30));
                    break;
                }
            }
            Console.WriteLine("Натиснiть \"Enter\", щоб повернутись назад.");
            string c = Console.ReadLine();
            Console.Clear();
            return;
        }

        static void SubjectSelection(List<Group> groups, List<Subject> subjects)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
            if (subjects.Count == 0)
            {
                Console.WriteLine("Ще жодного не додано.");
                Console.WriteLine("Натиснiть \"Enter\", щоб повернутись назад.");
                string c = Console.ReadLine();
                return;
            }
            Console.WriteLine("Введiть число, пiд яким знаходиться обраний вами предмет, щоб переглянути iнформацiю про нього ");
            for (int i = 0; i < subjects.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {subjects[i].SubjectName()}");
            }

            Console.Write("Вибір: ");
            int choice = int.Parse(Console.ReadLine());
            Console.Clear();
            for (int i = 0; i < subjects.Count; i++)
            {
                if (i == choice - 1)
                {
                    Console.WriteLine($"\n{subjects[i].SubjectName()} ({subjects[i].SubjectDescription()})");
                    Console.WriteLine(new String('_', 103));
                    Console.Write("Дата пари                     |");
                    foreach (var item in subjects[i].Lessons())
                    {

                        Console.Write(" {0,11} |", item.Date().ToShortDateString());
                    }
                    Console.WriteLine("\n" + new String('-', 103));
                    Console.Write("Прiзвище/Тип пари             |");
                    foreach (var item in subjects[i].Lessons())
                    {
                        Console.Write(" {0,11} |", item.Type());
                    }

                    Console.Write(" К-ть пропущених | К-ть балiв |\n");
                    Console.Write(new String('-', 103));
                    foreach (var group in subjects[i].GroupsNames())
                    {

                        foreach (var student in group.Students())
                        {
                            Console.Write($"\n{student.NameSurname(), -30}|");
                            string email = student.Email();

                            foreach (var lesson in subjects[i].Lessons())
                            {
                                String outsymbol = null;
                                foreach (var absent in lesson.Absent())
                                {
                                    if (email == absent)
                                    {
                                        outsymbol = "н ";
                                        break;
                                    }
                                }
                                if (outsymbol == null)
                                {
                                    foreach (var score in lesson.Grade())
                                    {
                                        if (email == score.StudEmail())
                                        {
                                            outsymbol = $" {score._Grade()} ";
                                            break;
                                        }
                                    }
                                }
                                Console.Write($" {outsymbol,11} |");
                            }
                            Console.Write($" {subjects[i].GetAbsent(email),15} | {subjects[i].GetGrade(email),10} |");
                            Console.Write("\n" + new String('-', 103));
                        }
                    }

                    break;
                }
            }
            Console.WriteLine("\nНатиснiть \"Enter\", щоб повернутись назад.");
            string end = Console.ReadLine();
            Console.Clear();
            return;
        }

        static void Serializer(List<Group> groups, List<Subject> subjects)
        {
            try
            {
                File.WriteAllText("GroupsSerialize.json", JsonConvert.SerializeObject(groups));
                File.WriteAllText("SubjectsSerialize.json", JsonConvert.SerializeObject(subjects));
                Console.WriteLine("Дані успішно збережено.\nНатисніть \"Enter\" для завершення програми.");
            }
            catch
            {
                throw new Exception("Нажаль не вдалось зберегти дані.\nНатисніть \"Enter\" для завершення програми.");
            }
            
        }

        static (List<Group>? groups, List<Subject>? subjects) Deserializer()
        { 
            try
            {
                File.ReadAllText("GroupsSerialize.json");
                File.ReadAllText("SubjectsSerialize.json");
            }
            catch
            {
                Console.WriteLine("Наразі збережені дані відсутні.\nДля продовження натисніть \"Enter\".");
                return (null, null);
            }
            JsonTextReader reader1 = new JsonTextReader(new StreamReader("GroupsSerialize.json"));
            reader1.SupportMultipleContent = true;
            JsonSerializer serializer1 = new JsonSerializer();
            List<Group>? groups = serializer1.Deserialize<List<Group>>(reader1);

            JsonTextReader reader2 = new JsonTextReader(new StreamReader("SubjectsSerialize.json"));
            reader2.SupportMultipleContent = true;
            JsonSerializer serializer2 = new JsonSerializer();
            List<Subject>? subjects = serializer2.Deserialize<List<Subject>>(reader2);
            return (groups, subjects);
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
            List<Group> groups = new List<Group>();
            List<Subject> subjects = new List<Subject>();
            int choice;
            while (true)
            {
                Console.WriteLine("Опцiї: \n1 - Створити новий журнал\n2 - Вiдкрити наявний журнал");
                Console.Write("Вибір: ");
                if (int.TryParse(Console.ReadLine(), out choice) == false)
                {
                    Console.WriteLine("Введiть будь ласка цифру варiанта вибору .");
                    continue;
                }
                else
                {
                    switch (choice)
                    {
                        case 1:
                            choice = GroupSubject(groups, subjects);
                            break;
                        case 2:
                            (groups, subjects) = Deserializer();
                            if (groups == null || subjects == null)
                            {
                                Console.WriteLine("Збережених файлів не виявлено.");
                            }
                            goto case 1;                         
                        default:
                            Console.WriteLine("А зря, треба було обирати що є, ану спробуй ще раз.");
                            break;
                    }
                    break;
                }
            }
            int i = 0;
            while (true)
            {
                switch (choice)
                {
                    case 1:
                        groups.Add(AddGroup());
                        choice = GroupSubject(groups, subjects);
                        break;
                    case 2:
                        subjects.Add(AddSubject(groups));
                        choice = GroupSubject(groups, subjects);
                        break;
                    case 3:
                        GroupSelection(groups, subjects);
                        choice = GroupSubject(groups, subjects);
                        break;
                    case 4:
                        SubjectSelection(groups, subjects);
                        choice = GroupSubject(groups, subjects);
                        break;
                    case 5:
                        Serializer(groups, subjects);
                        i++;
                        break;
                    default:
                        Console.WriteLine("А зря, треба було обирати що є, ану спробуй ще раз.");
                        break;
                }
                if (choice == 5 && i == 1)
                {
                    break;
                }
            }
            Console.ReadLine();
        }
    }
}