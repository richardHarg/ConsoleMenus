
namespace RLH.ConsoleMenus
{
    internal interface IMenu
    {
        /// <summary>
        /// Display name of the Menu
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Displays the configured menu items
        /// </summary>
        public Task OpenMenuAsync();
    }
}
