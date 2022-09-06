using System.Collections.Generic;

namespace Questions.Generator.Models
{
    public class MenuModel
    {
        public string Key { get; set; }
        public string Title { get; set; }
        public List<MenuModel> Menus { get; set; }
        public MenuType Type { get; set; } = MenuType.Classic;
        public List<MenuItemModel> Items { get; set; }
    }
}