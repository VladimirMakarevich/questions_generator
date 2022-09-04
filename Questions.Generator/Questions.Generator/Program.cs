using System;
using System.Globalization;
using System.Text;
using System.Threading;

namespace Questions.Generator
{
    public class Program
    {
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
            Console.WriteLine("Hello, it's Question Generator!");

            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu();
            }
        }

        private static bool MainMenu()
        {
            Console.WriteLine("Выбери способ:");
            Console.WriteLine("1) По типам");
            Console.WriteLine("2) По категориям");
            Console.WriteLine("3) По методам");
            Console.WriteLine("4) Help");
            Console.WriteLine("5) Clear Console");
            Console.WriteLine("6) Exit");
            Console.Write("\r\nВыбранный вариант: ");

            switch (Console.ReadLine())
            {
                case "1":
                    PrintTypesMenu();
                    return true;
                case "2":
                    PrintCategoriesMenu();
                    return true;
                case "3":
                    PrintMethodsMenu();
                    return true;
                case "4":
                    PrintHelpMenu();
                    return true;
                case "5":
                    ClearConsole();
                    return true;
                case "6":
                    return false;
                default:
                    return true;
            }
        }

        private static void PrintTypesMenu()
        {
            Console.Write("\r\n");
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = TypesMenu();
            }
        }

        private static void PrintCategoriesMenu()
        {
            Console.Write("\r\n");
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = CategoriesMenu();
            }
        }

        private static void PrintMethodsMenu()
        {
            Console.Write("\r\n");
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MethodsMenu();
            }
        }

        private static void PrintHelpMenu()
        {
            Console.Write("\r\n");
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = HelpMenu();
            }
        }

        private static bool TypesMenu()
        {
            Console.WriteLine("\r\nВыбери тип:");
            Console.WriteLine("1) О причине.");
            Console.WriteLine("2) О цели.");
            Console.WriteLine("3) О свойствах.");
            Console.WriteLine("4) О способе.");
            Console.WriteLine("5) О сущости.");
            Console.WriteLine("5) Выход в главное меню.");
            Console.Write("\r\nВыбранный вариант: ");

            switch (Console.ReadLine())
            {
                case "1":
                    GenerateQuestion("Почему");
                    return true;
                case "2":
                    Console.WriteLine("Зачем? К чему?");
                    return true;
                case "3":
                    Console.WriteLine("Какой? Какая? Какое?");
                    return true;
                case "4":
                    Console.WriteLine("Как? Каким Образом?");
                    return true;
                case "5":
                    GenerateQuestion("Что");
                    return true;
                case "6":
                    return false;
                default:
                    return true;
            }
        }

        private static bool CategoriesMenu()
        {
            Console.WriteLine("Выбери категорию:");
            Console.WriteLine("1) Выход в главное меню.");
            Console.Write("\r\nВыбранный вариант: ");

            switch (Console.ReadLine())
            {
                case "1":
                    return false;
                default:
                    return true;
            }
        }

        private static bool MethodsMenu()
        {
            Console.WriteLine("Выбери метод:");
            Console.WriteLine("1) Выход в главное меню.");
            Console.Write("\r\nВыбранный вариант: ");

            switch (Console.ReadLine())
            {
                case "1":
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
            Console.WriteLine("5) Выход в главное меню.");
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
                case "5":
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
            Console.Write($"Введите текст к которому хотите задать вопрос: {question} ");
            return Console.ReadLine();
        }

        private static void DisplayResult(string message, string question)
        {
            Console.WriteLine($"\r\nВаш вопрос: {question} {message}?");
            Console.Write("\r\nPress Enter to return to Menu.");
            Console.ReadLine();
        }
    }
}
