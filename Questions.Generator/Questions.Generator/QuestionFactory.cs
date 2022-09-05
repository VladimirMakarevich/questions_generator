using System;
using System.Collections.Generic;
using System.Linq;

namespace Questions.Generator
{
    public class QuestionFactory
    {
        private static List<MenuModel> MenuModels = new List<MenuModel>();

        public static void GenerateQuestions()
        {
            MenuModels = new List<MenuModel>()
            {
                new MenuModel()
                {
                    Key = "1",
                    Title = "По типам",
                    Type = MenuType.Classic,
                    Menus = new List<MenuModel>()
                    {
                        new MenuModel()
                        {
                            Key = "1",
                            Title = "О причине",
                            Type = MenuType.Classic,
                            Items = new List<MenuItemModel>()
                            {
                                new MenuItemModel()
                                {
                                    Key = "1",
                                    Item = "Почему"
                                }
                            }
                        },
                        new MenuModel()
                        {
                            Key = "2",
                            Title = "О цели",
                            Type = MenuType.Classic,
                            Items = new List<MenuItemModel>()
                            {
                                new MenuItemModel()
                                {
                                    Key = "1",
                                    Item = "Зачем"
                                },
                                new MenuItemModel()
                                {
                                    Key = "2",
                                    Item = "К чему"
                                }
                            }
                        },
                        new MenuModel()
                        {
                            Key = "3",
                            Title = "О свойствах",
                            Type = MenuType.Classic,
                            Items = new List<MenuItemModel>()
                            {
                                new MenuItemModel()
                                {
                                    Key = "1",
                                    Item = "Какой"
                                },
                                new MenuItemModel()
                                {
                                    Key = "2",
                                    Item = "Какая"
                                },
                                new MenuItemModel()
                                {
                                    Key = "3",
                                    Item = "Какое"
                                }
                            }
                        },
                        new MenuModel()
                        {
                            Key = "4",
                            Title = "О способе",
                            Type = MenuType.Classic,
                            Items = new List<MenuItemModel>()
                            {
                                new MenuItemModel()
                                {
                                    Key = "1",
                                    Item = "Как"
                                },
                                new MenuItemModel()
                                {
                                    Key = "2",
                                    Item = "Каким образом"
                                }
                            }
                        },
                        new MenuModel()
                        {
                            Key = "5",
                            Title = "О сущости",
                            Type = MenuType.Classic,
                            Items = new List<MenuItemModel>()
                            {
                                new MenuItemModel()
                                {
                                    Key = "1",
                                    Item = "Что"
                                }
                            }
                        }
                    }
                }
            };

            PrintGenericMenu();
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

        private static bool PrintMenu(MenuModel menuModel = null)
        {
            var currentMenus = new List<MenuModel>();
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
                currentMenus = MenuModels;
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

            var result =
                currentMenus.FirstOrDefault(x => x.Key.ToLowerInvariant().Equals(userOption.ToLowerInvariant()));
            if (result == null)
            {
                return true;
            }

            PrintGenericMenu(result);
            return true;
        }
    }

    public class MenuModel
    {
        public string Key { get; set; }
        public string Title { get; set; }
        public List<MenuModel> Menus { get; set; } = new List<MenuModel>();
        public MenuType Type { get; set; } = MenuType.Classic;
        public List<MenuItemModel> Items { get; set; }
    }

    public class MenuItemModel
    {
        public string Key { get; set; }
        public string Item { get; set; }
    }

    public enum MenuType
    {
        Classic = 1,
        Random = 2
    }
}