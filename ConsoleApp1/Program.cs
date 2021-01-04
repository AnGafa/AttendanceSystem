using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            ProgramInterface intf = new ProgramInterface();
            Menu subMenu = new Menu();
            subMenu.AddMenuItem("1. add attendance", intf.ExecuteSubOption1);
            subMenu.AddMenuItem("2. add group", intf.ExecuteSubOption2);
            subMenu.AddMenuItem("3. get lift of attendance by student", intf.ExecuteSubOption2);


            Menu mainMenu = new Menu();
            mainMenu.AddMenuItem("Login", subMenu.Run);
            mainMenu.AddMenuItem("Exit", intf.ExecuteOption2);

            

            mainMenu.Run();
        }
    }


    /// <summary>
    /// Interfaces menu with application. Each menu item will have a corresponding 
    /// method in the interface class
    /// </summary>
    class ProgramInterface
    {

        public void ExecuteOption1()
        {
            GroupService gs = new GroupService();

            foreach (var g in gs.GetGroup())
            {
                Console.WriteLine($"{g.GroupID}. {g.Name}");
            }

            Console.WriteLine("Input group ID");
            Console.WriteLine("Input ...");
        }

        public void ExecuteOption2()
        {
            Group myGroup = new Group();

            Console.WriteLine("Input group name");
            myGroup.Name = Console.ReadLine();

            Console.WriteLine("Input course name");
            myGroup.Course = Console.ReadLine();

            GroupService gser = new GroupService();
            gser.Add(myGroup);
        }
        public void ExecuteSubOption1()
        {
            Console.WriteLine("Executing Sub Option 1");
        }

        public void ExecuteSubOption2()
        {
            Console.WriteLine("Executing Sub Option 2");
        }
    }


    /// <summary>
    /// Menu Item. A menu item will have:
    ///     a text that it will display in the menu
    ///     a delegate that it will call when the menu is selected
    /// </summary>
    class MenuItem
    {
        public delegate void executingMethod();

        public string Display { get; }
        public executingMethod Exec { get; }

        private MenuItem() { }

        public MenuItem(string displayName, executingMethod execMethod)
        {
            this.Display = displayName;
            this.Exec = execMethod;
        }
    }



    /// <summary>
    /// The menu class
    /// </summary>
    class Menu
    {
        /// <summary>
        /// Menu is a lsit of menu items
        /// Dictionary is used as the menu item will have an associated integer that will be used
        /// to select the item
        /// e.g.
        /// 1. Menu item 1
        /// the menu item number 0 is reserved to exit
        /// </summary>
        private readonly Dictionary<int, MenuItem> menuItems = new Dictionary<int, MenuItem>();

        /// <summary>
        /// menu itenm number
        /// </summary>
        int menuOption = 0;

        /// <summary>
        /// menu execution method.
        /// will run until 0 is selected
        /// execution cycle is made up of three steps
        /// 1. display the menu
        /// 2. get the selected option
        /// 3. execture the selected option
        /// </summary>
        public void Run()
        {
            int option = 0;

            do
            {
                DisplayMenu();
                option = GetMenuSelection();
                ProcessSelection(option);
            }
            while (option != 0);
        }

        /// <summary>
        /// Adds a new menu item to the menu
        /// </summary>
        /// <param name="menuText">display text for the menu item</param>
        /// <param name="execMethod">method to call when selected</param>
        public void AddMenuItem(string menuText, MenuItem.executingMethod execMethod)
        {
            MenuItem op = new MenuItem(menuText, execMethod);
            menuItems.Add(menuOption, op);
            // increment the menu option after each addition
            menuOption++;
        }

        /// <summary>
        /// menu constructor. add the exit menu item
        /// </summary>
        public Menu()
        {
            AddMenuItem("Exit", OutputExit);
        }

        /// <summary>
        /// Dsiplays the menu
        /// iterates through the dictionary and prints the number folloed by the display text
        /// </summary>
        private void DisplayMenu()
        {
            System.Console.Clear();

            foreach (KeyValuePair<int, MenuItem> entry in menuItems)
            {
                System.Console.WriteLine(entry.Key + ".  " + entry.Value.Display);
            }
        }

        /// <summary>
        /// gets the menu selction
        /// </summary>
        /// <returns>the menu selection</returns>
        private int GetMenuSelection()
        {
            string ret = System.Console.ReadLine();
            int option = 0;
            
            // try to parse the entered text to int. if exception is thrown, 
            // display error message and dusplay the menu items again
            try
            {
                option = int.Parse(ret);
            }
            catch (Exception)
            {
                System.Console.WriteLine("enter the option number to select. Press any Key to continue...");
                System.Console.ReadLine();
                this.DisplayMenu();
            }

            return option;
        }

        /// <summary>
        /// pocesses the menu selection
        /// </summary>
        /// <param name="option"></param>
        private void ProcessSelection(int option)
        {
            // make sure that selection is a valid one
            if(option < 0 || option > this.menuOption)
            {
                System.Console.WriteLine("Invalid option. Press any Key to continue...");
                System.Console.ReadLine();
                System.Console.Clear();
                return;
            }

            // execute the associated menu item function
            this.menuItems[option].Exec();

            // workaround for cascading menus bug
            if(option != 0)
                System.Console.ReadLine();
        }

        /// <summary>
        /// menthod called ehen exit is selected
        /// </summary>
        private void OutputExit()
        {
            Console.WriteLine("exiting ...");
        }
    }
}
