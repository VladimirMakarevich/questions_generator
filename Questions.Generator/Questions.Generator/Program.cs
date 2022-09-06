using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;

namespace Questions.Generator
{
    public class Program
    {
        private static readonly string[] AllQuestions = new List<string>()
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

        private static readonly string[] ReadingQuestions = new List<string>()
        {
            "Что я могу сделать лучше",
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

            QuestionFactory.GenerateQuestions();

            // PrintGenericMenu(MainMenu);
        }

        private static void PrintGenericMenu(Func<bool> menu)
        {
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = menu();
            }
        }

        private static bool ReadingMenu()
        {
            return GenerateRandomQuestion(GetNextQuestion(ReadingQuestions));
        }

        private static void PrintRandomQuestion()
        {
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = GenerateRandomQuestion(GetNextQuestion(AllQuestions));
            }
        }

        private static string GetNextQuestion(string[] questions)
        {
            var random = new Random();
            var index = random.Next(0, questions.Length);

            return questions[index];
        }

        private static bool MainMenu()
        {
            Console.WriteLine("Выбери способ:\n");
            Console.WriteLine("1) По типам.");
            Console.WriteLine("2) По категориям.");
            Console.WriteLine("3) По методам.");
            Console.WriteLine("4) Случайный вопрос.");
            Console.WriteLine("5) Вопросы для чтения.");
            Console.WriteLine("help) Помощь.");
            Console.WriteLine("clear) Очистить Консоль.");
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
                    PrintGenericMenu(ReadingMenu);
                    return true;
                case "help":
                    PrintGenericMenu(HelpMenu);
                    return true;
                case "clear":
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
            Console.WriteLine("ENTER) Получить следующий вопрос.");
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
            Console.WriteLine("Выбери вариант:");
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

        public static void GenerateQuestion(string question)
        {
            DisplayResult(CaptureInput(question), question);
        }

        public static string CaptureInput(string question)
        {
            Console.Write($"Введите текст к которому хотите задать вопрос: {question} ");
            
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Black;
            
            var text = Console.ReadLine();
            
            Console.ResetColor();

            return text;
        }

        public static void DisplayResult(string message, string question)
        {
            char questionSymbol = ' ';
            if (!string.IsNullOrWhiteSpace(message))
            {
                var lastSymbol = message[^1];
                if (!lastSymbol.Equals('?'))
                {
                    questionSymbol = '?';
                }
                else
                {
                    questionSymbol = ' ';
                }   
            }

            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($"\r\nВаш вопрос: {question} {message}{questionSymbol}".TrimEnd());
            Console.ResetColor();
            Console.Write("\r\nНажмите Enter что бы вернуться в предыдущее меню.");
            Console.ReadLine();
            Console.WriteLine($"\r\n");
        }
        
        //Returns null if ESC key pressed during input.
        private static string ReadLineWithCancel()
        {
            string result = null;

            var buffer = new StringBuilder();

            //The key is read passing true for the intercept argument to prevent
            //any characters from displaying when the Escape key is pressed.
            ConsoleKeyInfo info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Enter && info.Key != ConsoleKey.Escape)
            {
                Console.Write(info.KeyChar);
                buffer.Append(info.KeyChar);
                info = Console.ReadKey(true);
            } 

            if (info.Key == ConsoleKey.Enter)
            {
                result = buffer.ToString();
            }

            return result;
        }
    }
}