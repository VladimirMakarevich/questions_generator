using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Questions.Generator.Models;

namespace Questions.Generator
{
    public static class QuestionFactory
    {
        private static List<MenuModel> _menuModels = new List<MenuModel>();

        public static void GenerateQuestions()
        {
            _menuModels = Read($"{AppDomain.CurrentDomain.BaseDirectory}data.json");

            PrintGenericMenu();
        }

        private static List<MenuModel> Read(string path)
        {
            using var file = new StreamReader(path);
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

        private static void PrintGenericMenu()
        {
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = PrintMenu();
            }
        }

        private static void PrintGenericMenu(MenuModel menuModel)
        {
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = PrintMenu(menuModel);
            }
        }

        private static bool GenerateQuestions(List<MenuItemModel> items)
        {
            Console.WriteLine("Выберите вариант:");
            foreach (var item in items)
            {
                Console.WriteLine($"{item.Key}) {item.Item}?");
            }

            Console.WriteLine($"0) Назад.");
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
                items.FirstOrDefault(x => x.Key.ToLowerInvariant().Equals(userOption.ToLowerInvariant()));
            if (selectedItem == null)
            {
                return true;
            }

            Program.GenerateQuestion(selectedItem.Item);
            return true;
        }

        private static bool GenerateRandomQuestion(MenuModel menu)
        {
            bool showMenu = true;
            while (showMenu)
            {
                ClearConsole();
                var question = GetNextQuestion(menu.Items.ToArray());
                Console.WriteLine($"Вопрос: {question.Item}?");
                Console.WriteLine($"\r");
                Console.WriteLine("1) Выбрать этот вопрос.");
                Console.WriteLine("ENTER) Получить следующий вопрос.");
                Console.WriteLine("0) Выход в главное меню.");
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
            }

            return true;
        }

        private static MenuItemModel GetNextQuestion(MenuItemModel[] questions)
        {
            var random = new Random();
            var index = random.Next(0, questions.Length);

            return questions[index];
        }

        private static bool PrintMenu(MenuModel menuModel = null)
        {
            ClearConsole();
            List<MenuModel> currentMenus;
            if (menuModel != null)
            {
                currentMenus = menuModel.Menus;

                if (currentMenus == null || !currentMenus.Any() && menuModel.Items.Any())
                {
                    return GenerateQuestions(menuModel.Items);
                }
            }
            else
            {
                currentMenus = _menuModels;
            }

            Console.WriteLine("Выберите вариант:");
            foreach (var menu in currentMenus)
            {
                Console.WriteLine($"{menu.Key}) {menu.Title}.");
            }

            Console.WriteLine($"0) Назад.");
            Console.Write("\r\nВыбранный вариант: ");

            var userOption = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(userOption))
            {
                return true;
            }

            if (userOption.ToLowerInvariant().Equals("0".ToLowerInvariant()))
            {
                return false;
            }

            var currentMenu = currentMenus.FirstOrDefault(x => x.Key.ToLowerInvariant().Equals(userOption.ToLowerInvariant()));
            if (currentMenu == null)
            {
                return true;
            }

            if (currentMenu.Type == MenuType.Random)
            {
                return GenerateRandomQuestion(currentMenu);
            }

            PrintGenericMenu(currentMenu);
            return true;
        }
        
        private static void ClearConsole()
        {
            Console.Clear();
        }
    }
}