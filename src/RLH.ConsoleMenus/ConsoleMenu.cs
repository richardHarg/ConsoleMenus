
namespace RLH.ConsoleMenus
{
    /// <summary>
    /// Base abstract class for IMenu classes. Holds common functionality 
    /// </summary>
    public abstract class ConsoleMenu : IDisposable
    {
        public string Title { get; private set; }
        private int _currentMenuItem;
        private string _exitSelectionText = "Back to Previous Menu";
        private Dictionary<int, string> _menus = new Dictionary<int, string>();


        private bool disposedValue;

        /// <summary>
        /// Creates a new 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="menuStartingIndex"></param>
        public ConsoleMenu(string title,int menuStartingIndex = 1)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException($"'{nameof(title)}' cannot be null or whitespace.", nameof(title));
            }
            if (menuStartingIndex <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(menuStartingIndex));
            }

            Title = title;
            _currentMenuItem = menuStartingIndex;
        }


        /// <summary>
        /// Changes the default "Back to Previous Menu" text to a custom value
        /// </summary>
        /// <param name="text">Value to set exit selection message to</param>
        /// <exception cref="ArgumentException">Thrown if the passed 'text' parameter is null/blank or whitespace</exception>
        protected void SetCustomExitSelection(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentException($"'{nameof(text)}' cannot be null or whitespace.", nameof(text));
            }

            _exitSelectionText = text;
        }

        /// <summary>
        /// Creates a new menu selection
        /// </summary>
        /// <param name="name">string describing this menu item</param>
        /// <param name="customIndex">Optional custom index value to use instead of incrementing</param>
        /// <returns></returns>
        protected ConsoleMenu AddMenuSelection(string name,int? customIndex = null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            if (customIndex.HasValue)
            {
                if (_menus.ContainsKey(customIndex.Value))
                {
                    throw new ArgumentException("CustomIndex value passed is already in use");
                }
                _menus.Add(customIndex.Value, name);
            }
            else
            {
                _menus.Add(_currentMenuItem, name);
                _currentMenuItem++;
            }
            return this;
        }


        /// <summary>
        /// Displays the configured menu options
        /// </summary>
        /// <param name="message">Optional message to display before prompting for input</param>
        public void Show(string message = "")
        {
            Console.Clear();
            // Write the title
            Console.WriteLine(Title);

            // Loop through all menu items and display the key/text for each
            foreach (var item in _menus)
            {
                Console.WriteLine($"{item.Key} - {item.Value}");
            }

            // Show the exit selection text
            Console.WriteLine($"0 - {_exitSelectionText}");

            // If an optional message has been passed also display this
            if (string.IsNullOrWhiteSpace(message) == false)
            {
                Console.WriteLine(message);
                Console.WriteLine();
            }
        }

        protected void PressAnyKey()
        {
            Console.WriteLine("Press any key to continue....");
            Console.ReadKey();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~ConsoleMenu()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
