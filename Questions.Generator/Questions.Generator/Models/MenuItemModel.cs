namespace Questions.Generator.Models
{
    public class MenuItemModel
    {
        public string Key { get; set; }
        public string Item { get; set; }
        public bool IsQuestion { get; set; } = true;
        public string Description { get; set; }
    }
}