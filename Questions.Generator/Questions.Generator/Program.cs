using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;

namespace Questions.Generator
{
    public class Program
    {
        private static readonly string[] Questions = new List<string>()
        {
            "Что",
            "Почему",
            "Зачем",
            "К чему",
            "Какой",
            "Какая",
            "Какое",
            "Как",
            "Каким Образом",
            "Что"
        }.ToArray();

        private static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var enc1251 = Encoding.GetEncoding(1251);

            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = enc1251;

            var cultureInfo = new CultureInfo("ru-RU");
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;

            // Creates and initializes the CultureInfo which uses the international sort.
            Console.WriteLine("Привет, это генератор вопрос для системного мышления!\n");

            PrintGenericMenu(MainMenu);
        }

        private static void PrintGenericMenu(Func<bool> menu)
        {
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = menu();
            }
        }
        
        private static void PrintRandomQuestion()
        {
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = GenerateRandomQuestion(GetNextQuestion());
            }
        }

        private static string GetNextQuestion()
        {
            var random = new Random();
            var index = random.Next(0, Questions.Length);

            return Questions[index];
        }
        
        private static bool MainMenu()
        {
            Console.WriteLine("Выбери способ:\n");
            Console.WriteLine("1) По типам.");
            Console.WriteLine("2) По категориям.");
            Console.WriteLine("3) По методам.");
            Console.WriteLine("4) Случайный вопрос.");
            Console.WriteLine("5) Помощь.");
            Console.WriteLine("9) Очистить Консоль.");
            Console.WriteLine("0) Выход.");
            Console.Write("\r\nВыбранный вариант: ");

            switch (Console.ReadLine())
            {
                case "1":
                    PrintGenericMenu(TypesMenu);
                    return true;
                case "2":
                    PrintGenericMenu(CategoriesMenu);
                    return true;
                case "3":
                    PrintGenericMenu(MethodsMenu);
                    return true;
                case "4":
                    PrintRandomQuestion();
                    return true;
                case "5":
                    PrintGenericMenu(HelpMenu);
                    return true;
                case "9":
                    ClearConsole();
                    return true;
                case "0":
                    return false;
                default:
                    return true;
            }
        }

        private static bool GenerateRandomQuestion(string question)
        {
            Console.WriteLine($"\r");
            Console.WriteLine($"Вопрос: {question}?");
            Console.WriteLine($"\r");
            Console.WriteLine("1) Выбрать этот вопрос.");
            Console.WriteLine("2) Сгенерировать следующий вопрос.");
            Console.WriteLine("0) Выход в главное меню.");
            Console.Write("\r\nВыбранный вариант: ");

            switch (Console.ReadLine())
            {
                case "1":
                    DisplayResult(CaptureInput(question), question);
                    return true;
                case "2":
                    return true;
                case "0":
                    return false;
                default:
                    return true;
            }
        }
        
        private static bool TypesMenu()
        {
            Console.WriteLine("Выберите о чем будет вопрос:");
            Console.WriteLine("1) О причине.");
            Console.WriteLine("2) О цели.");
            Console.WriteLine("3) О свойствах.");
            Console.WriteLine("4) О способе.");
            Console.WriteLine("5) О сущости.");
            Console.WriteLine("0) Выход в главное меню.");
            Console.Write("\r\nВыбранный вариант: ");

            switch (Console.ReadLine())
            {
                case "1":
                    GenerateQuestion("Почему");
                    return true;
                case "2":
                    GenerateQuestions("Зачем", "К чему");
                    return true;
                case "3":
                    GenerateQuestions("Какой", "Какая", "Какое");
                    return true;
                case "4":
                    GenerateQuestions("Как", "Каким Образом");
                    return true;
                case "5":
                    GenerateQuestion("Что");
                    return true;
                case "0":
                    return false;
                default:
                    return true;
            }
        }

        private static void GenerateQuestions(string v1, string v2, string v3)
        {
            Console.WriteLine("Выберите вариант:");
            Console.WriteLine($"1) {v1}?");
            Console.WriteLine($"2) {v1}?");
            Console.WriteLine($"3) {v3}?");
            Console.WriteLine($"0) Назад.");
            Console.Write("\r\nВыбранный вопрос: ");

            switch (Console.ReadLine())
            {
                case "1":
                    GenerateQuestion(v1);
                    return;
                case "2":
                    GenerateQuestion(v2);
                    return;
                case "3":
                    GenerateQuestion(v3);
                    return;
                case "0":
                    return;
                default:
                    return;
            }
        }

        private static void GenerateQuestions(string v1, string v2)
        {
            Console.WriteLine("Выберите вариант:");
            Console.WriteLine($"1) {v1}?");
            Console.WriteLine($"2) {v2}?");
            Console.WriteLine($"0) Назад.");
            Console.Write("\r\nВыбранный вопрос: ");

            switch (Console.ReadLine())
            {
                case "1":
                    GenerateQuestion(v1);
                    return;
                case "2":
                    GenerateQuestion(v2);
                    return;
                case "0":
                    return;
                default:
                    return;
            }
        }

        private static bool CategoriesMenu()
        {
            Console.WriteLine("Выбери категорию:");
            Console.WriteLine("0) Выход в главное меню.");
            Console.Write("\r\nВыбранный вариант: ");

            switch (Console.ReadLine())
            {
                case "0":
                    return false;
                default:
                    return true;
            }
        }

        private static bool MethodsMenu()
        {
            Console.WriteLine("Выбери метод:");
            Console.WriteLine("0) Выход в главное меню.");
            Console.Write("\r\nВыбранный вариант: ");

            switch (Console.ReadLine())
            {
                case "0":
                    return false;
                default:
                    return true;
            }
        }

        private static bool HelpMenu()
        {
            Console.WriteLine("Выбери способ:");
            Console.WriteLine("1) Проблема вопрошания.");
            Console.WriteLine("2) Проблема XY.");
            Console.WriteLine("3) Что такое предмет вопрошания.");
            Console.WriteLine("4) Часто задоваемые вопросы.");
            Console.WriteLine("0) Выход в главное меню.");
            Console.Write("\r\nВыбранный вариант: ");

            switch (Console.ReadLine())
            {
                case "1":
                    Console.WriteLine("Тут будет 'Проблема вопрошания'");
                    return true;
                case "2":
                    Console.WriteLine("Тут будет 'Проблема XY'");
                    return true;
                case "3":
                    Console.WriteLine("Тут будет 'Что такое предмет вопрошания'");
                    return true;
                case "4":
                    Console.WriteLine("Тут будет 'Часто задоваемые вопросы'");
                    return true;
                case "0":
                    return false;
                default:
                    return true;
            }
        }

        private static void ClearConsole()
        {
            Console.Clear();
        }

        private static void GenerateQuestion(string question)
        {
            DisplayResult(CaptureInput(question), question);
        }

        private static string CaptureInput(string question)
        {
            Console.WriteLine($"\n");
            Console.Write($"Введите текст к которому хотите задать вопрос: {question} ");
            
            return Console.ReadLine();
        }

        private static void DisplayResult(string message, string question)
        {
            Console.WriteLine($"\r\nВаш вопрос: {question} {message}?");
            Console.Write("\r\nНажмите Enter что бы вернуться в предыдущее меню.");
            Console.ReadLine();
            Console.WriteLine($"\r\n");
        }
    }
}