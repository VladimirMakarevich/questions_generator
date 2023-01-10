using Newtonsoft.Json;
using Questions.Generator.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Questions.Generator
{
    public static class QuestionFactory
    {
        private static List<MenuModel> _menuModels = new List<MenuModel>();
        private static string _menuTitle = "Главное меню >> ";

        public static void GenerateQuestions(string filePath = null)
        {
            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
            {
                filePath = $"{AppDomain.CurrentDomain.BaseDirectory}data.json";
            }

            _menuModels = Read(filePath);

            PrintGenericMenu();
        }

        private static void PrintGenericMenu(MenuModel menuModel = null, string menuTitle = null)
        {
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = PrintMenu(menuModel, menuTitle ?? _menuTitle);
            }
        }

        private static bool PrintQuestions(MenuModel menuModel, string menuTitle)
        {
            PrintCurrentTitle(menuModel, menuTitle);

            foreach (var item in menuModel.Items ?? new List<MenuItemModel>())
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{item.Key}) {item.Item}?");
                Console.ResetColor();
                if (!string.IsNullOrWhiteSpace(item.Description))
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"- {item.Description}");
                    Console.ResetColor();
                }
            }

            PrintBack();

            Console.Write("\r\nВыбранный вопрос: ");

            var userOption = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(userOption))
            {
                return true;
            }

            if (userOption.ToLowerInvariant().Equals("0".ToLowerInvariant()))
            {
                return false;
            }

            var selectedItem =
                menuModel.Items.FirstOrDefault(x => x.Key.ToLowerInvariant().Equals(userOption.ToLowerInvariant()));
            if (selectedItem == null)
            {
                return true;
            }

            Program.GenerateQuestion(selectedItem.Item);
            return true;
        }


        private static bool GenerateHelperQuestion(MenuModel menu, string menuTitle = null)
        {
            var items = new List<MenuItemModel>();
            bool isInitRandom = true;
            bool showMenu = true;
            while (showMenu)
            {
                ClearConsole();

                PrintCurrentTitle(menu, menuTitle);

                var question = GetNextItem(menu.Items.ToArray(), isInitRandom, items);
                if (question != null)
                {
                    Console.Write($"Вопрос: ");
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($">>> {question.Item}? <<<");
                    Console.WriteLine($"\r");
                    Console.ResetColor();
                    if (!string.IsNullOrWhiteSpace(question.Description))
                    {
                        Console.WriteLine($"\r");
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine($"- {question.Description}");
                        Console.ResetColor();
                    }
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($">>> Список пустой, нажмите '1' что бы начать сначало. <<<");
                    Console.WriteLine($"\r");
                    Console.ResetColor();
                }

                Console.WriteLine($"\r");
                Console.WriteLine("1) Начать сначало.");
                Console.WriteLine("ENTER) Получить следующий пункт.");

                PrintBack();

                Console.Write("\r\nВыбранный вариант: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        items = new List<MenuItemModel>();
                        isInitRandom = true;
                        continue;
                    case "0":
                        showMenu = false;
                        break;
                }

                isInitRandom = false;
            }

            return true;
        }

        private static bool GenerateRandomQuestion(MenuModel menu, string menuTitle = null)
        {
            bool isInitRandom = true;
            bool showMenu = true;
            while (showMenu)
            {
                ClearConsole();

                PrintCurrentTitle(menu, menuTitle);

                var question = GetNextRandomItem(menu.Items.ToArray(), isInitRandom);
                Console.Write($"Вопрос: ");
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($">>> {question.Item}? <<<");
                Console.WriteLine($"\r");
                Console.ResetColor();
                if (!string.IsNullOrWhiteSpace(question.Description))
                {
                    Console.WriteLine($"\r");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"- {question.Description}");
                    Console.ResetColor();
                }
                Console.WriteLine($"\r");
                Console.WriteLine("1) Выбрать этот вопрос.");
                Console.WriteLine("ENTER) Получить следующий вопрос.");

                PrintBack();

                Console.Write("\r\nВыбранный вариант: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        Program.DisplayResult(Program.CaptureInput(question.Item), question.Item);
                        break;
                    case "0":
                        showMenu = false;
                        break;
                }

                isInitRandom = false;
            }

            return true;
        }

        private static string PrintCurrentTitle(MenuModel menuModel, string menuTitle = null)
        {
            if (menuModel != null)
            {
                Console.Write($"{menuTitle}");
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"{menuModel.Title}");
                Console.ResetColor();
                Console.Write($" >> ");
                Console.Write($"Выберите вариант:");
                Console.Write("\n");
                Console.WriteLine($"\r");
                return $"{menuTitle}{menuModel.Title} >>> ";
            }
            else
            {
                Console.WriteLine($"{menuTitle}Выберите вариант:");
                Console.WriteLine($"\r");
                return menuTitle;
            }
        }

        private static bool PrintMenu(MenuModel menuModel = null, string menuTitle = null)
        {
            ClearConsole();
            List<MenuModel> currentMenus;
            if (menuModel != null)
            {
                currentMenus = menuModel.Menus;

                if (currentMenus == null || !currentMenus.Any() && menuModel.Items.Any())
                {
                    return PrintQuestions(menuModel, menuTitle);
                }
            }
            else
            {
                currentMenus = _menuModels;
            }

            menuTitle = PrintCurrentTitle(menuModel, menuTitle);

            foreach (var menu in currentMenus)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"{menu.Key}) {menu.Title}.");
                Console.ResetColor();
            }

            PrintBack(menuTitle);

            Console.Write("\r\nВыбранный вариант: ");

            var userOption = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(userOption))
            {
                return true;
            }

            if (_menuTitle.Equals(menuTitle))
            {
                if (userOption.ToLowerInvariant().Equals("exit".ToLowerInvariant()))
                {
                    return false;
                }
            }
            else
            {
                if (userOption.ToLowerInvariant().Equals("0".ToLowerInvariant()))
                {
                    return false;
                }
            }

            var currentMenu =
                currentMenus.FirstOrDefault(x => x.Key.ToLowerInvariant().Equals(userOption.ToLowerInvariant()));
            if (currentMenu == null)
            {
                return true;
            }

            if (currentMenu.Type == MenuType.Random)
            {
                return GenerateRandomQuestion(currentMenu, menuTitle);
            }

            if (currentMenu.Type == MenuType.Helper)
            {
                return GenerateHelperQuestion(currentMenu, menuTitle);
            }

            PrintGenericMenu(currentMenu, menuTitle);
            return true;
        }

        private static void PrintBack(string menuTitle = null)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\r");

            Console.WriteLine(_menuTitle.Equals(menuTitle) ? $"exit) Выход." : $"0) Назад.");

            Console.ResetColor();
        }

        private static MenuItemModel GetNextItem(MenuItemModel[] questions, bool isInitRandom, List<MenuItemModel> items)
        {
            MenuItemModel nextItem = null;
            if (isInitRandom || questions.Length == 1)
            {
                nextItem = questions[0];
                items.Add(nextItem);

                return nextItem;
            }

            var availableQuestions = questions.Where(x => items.All(c => c.Key != x.Key)).ToArray();
            if (!availableQuestions.Any())
            {
                return null;
            }

            var random = new Random();
            var index = random.Next(0, availableQuestions.Length);

            nextItem = availableQuestions[index];
            items.Add(nextItem);

            return nextItem;
        }

        private static MenuItemModel GetNextRandomItem(MenuItemModel[] questions, bool isInitRandom)
        {
            if (isInitRandom)
            {
                return questions[0];
            }

            var random = new Random();
            var index = random.Next(0, questions.Length);

            return questions[index];
        }

        private static void ClearConsole()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("----------------------------------");
            Console.WriteLine("           > МЕНЮ <");
            PrintSideBar();
            Console.WriteLine("----------------------------------");
            Console.ResetColor();
            Console.WriteLine("\r");
        }

        private static void PrintSideBar(List<MenuModel> menuModels = null, int count = 0)
        {
            if (menuModels == null)
            {
                menuModels = _menuModels;
            }

            var sb = new StringBuilder();

            if (count == default)
            {
                sb.Append("- ");
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    sb.Append("  - ");
                }
            }

            foreach (var menuModel in menuModels)
            {
                Console.WriteLine($"{sb}{menuModel.Title}");
                if (menuModel.Menus == null)
                {
                    continue;
                }

                /*
                PrintSideBar(menuModel.Menus, ++count);
                count = default;
                */
            }
        }

        private static List<MenuModel> Read(string path)
        {
            using (var file = new StreamReader(path))
            {
                try
                {
                    string json = file.ReadToEnd();

                    var data = JsonConvert.DeserializeObject<DataModel>(json);

                    return data?.Items ?? new List<MenuModel>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Problem reading file: {ex}.");
                    return null;
                }
            }
        }
    }
}