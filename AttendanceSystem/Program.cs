using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace AttendanceSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            ProgramInterface intf = new ProgramInterface();
            intf.Start();
        }
    }


    /// <summary>
    /// Interfaces menu with application. Each menu item will have a corresponding 
    /// method in the interface class
    /// </summary>
    class ProgramInterface
    {

        TeacherService teacherService = new TeacherService();
        AttendanceService attendanceService = new AttendanceService();
        GroupService groupService = new GroupService();
        LessonService lessonService = new LessonService();
        StudentService studentService = new StudentService();

        string loggedUserName = null;
        int loggedUserID = -1;

        public void Start()
        {
            Menu mainMenu = new Menu("School Administration System 2021");
            mainMenu.AddMenuItem("Login", Login);
            mainMenu.Run();
        }


        private void StartTeacherMaintenanceMenu()
        {
            Menu teacherMaintenanceMenu = new Menu("Hello  " + loggedUserName + loggedUserID.ToString());
            teacherMaintenanceMenu.AddMenuItem("Add attendance", ExecuteAddAttendance);
            teacherMaintenanceMenu.AddMenuItem("Add group", AddGroup);
            teacherMaintenanceMenu.AddMenuItem("Add student", AddStudent);
            teacherMaintenanceMenu.AddMenuItem("Add teacher", AddTeacher);
            //teacherMaintenanceMenu.AddMenuItem("Get list of attendance by student", ExecuteSubOption2);

            teacherMaintenanceMenu.Run();
        }


        public void Login()
        {

            Console.WriteLine("UserName:");
            var userName = Console.ReadLine();

            bool userExists = teacherService.DoesTeacherExist(userName);

            if (userExists)
            {
                System.Console.WriteLine("teacher exists");
                Console.WriteLine("Password:");
                var password = Console.ReadLine();
                int teacherID = teacherService.VerifyCredentials(userName, password);
                if(teacherID != 0)
                {
                    System.Console.WriteLine("successfully logged in");
                    loggedUserName = userName;
                    loggedUserID = teacherID;

                    StartTeacherMaintenanceMenu();

                    loggedUserName = null;
                }
                else
                {
                    System.Console.WriteLine("wrong password");
                }

            }
            else
                System.Console.WriteLine("teacher does not exist");


        }


        public void ExecuteAddAttendance()
        {

            foreach (var g in groupService.GetGroup())
            {
                Console.WriteLine($"{g.GroupID}. {g.Name}");
            }

            Console.WriteLine("Input group ID");
            int userGroupID = Convert.ToInt32(Console.ReadLine());

            bool userGroupExists = groupService.DoesGroupExist(userGroupID);

            if (userGroupExists)
            {
                DateTime now = DateTime.Now;

                int lessonID = lessonService.addNewLesson(userGroupID, now, loggedUserID);

                Console.WriteLine("input a or p");
                
                string studentAttendance = "";

                Console.WriteLine($"StudentID\tName\tSurname\tp/a");
                foreach (var s in studentService.GetStudentsFromGroup(userGroupID))
                {
                    do
                    {
                        studentAttendance = "";
                        Console.Write($"{s.StudentID}\t{s.Name}\t{s.Surname}\t");
                        studentAttendance = Console.ReadLine();
                    }
                    while (!studentAttendance.Equals("a") && !studentAttendance.Equals("p"));

                    attendanceService.SetStudentAttandanceRecord(lessonID, s.StudentID, studentAttendance);
                }

                Console.WriteLine("All records entered. Press any key to continue...");

            }
            else
                System.Console.WriteLine("Group ID does not exist");

        }

        public void AddGroup()
        {

            Console.WriteLine("Input group name");
            string groupName = Console.ReadLine();

            Console.WriteLine("Input course name");
            string courseName = Console.ReadLine();

            groupService.addNewGroup(groupName, courseName);
        }

        public void AddStudent()
        {
            foreach (var g in groupService.GetGroup())
            {
                Console.WriteLine($"{g.GroupID}. {g.Name}");
            }

            Console.WriteLine("Input student's group ID");

            int userStudentGroupID = Convert.ToInt32(Console.ReadLine());

            bool userStudentGroupExists = groupService.DoesGroupExist(userStudentGroupID);

            if (userStudentGroupExists)
            {
                Console.WriteLine("Input Student ID:");
                int userStudentID = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Input Student Name:");
                string userStudentName = Console.ReadLine();

                Console.WriteLine("Input Student Surname:");
                string userStudentSurname = Console.ReadLine();

                Console.WriteLine("Input Student Email:");
                string userStudentEmail = Console.ReadLine();

                try
                {
                    studentService.addNewStudent(userStudentID, userStudentName, userStudentSurname, userStudentEmail, userStudentGroupID);
                    Console.WriteLine("Record entered. Press any key to continue...");
                }
                catch
                {

                }
            }
            else
                System.Console.WriteLine("Group ID does not exist");
        }
        
        public void AddTeacher()
        {
            Console.WriteLine("Input Teacher ID:");
            int userTeacherID = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Input Teacher Username:");
            string userTeacherUsername = Console.ReadLine();

            Console.WriteLine("Input Teacher Password:");
            string userTeacherPassword = Console.ReadLine();

            Console.WriteLine("Input Teacher Name:");
            string userTeacherName = Console.ReadLine();

            Console.WriteLine("Input Teacher Surname:");
            string userTeacherSurname = Console.ReadLine();

            Console.WriteLine("Input Teacher Email:");
            string userTeacherEmail = Console.ReadLine();

            try
            {
                teacherService.addNewTeacher(userTeacherID, userTeacherUsername, userTeacherPassword, userTeacherName, userTeacherSurname, userTeacherEmail);
                Console.WriteLine("Record entered. Press any key to continue...");
            }
            catch
            {

            }
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

        private string menuTitle = "Menu Title";

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
        public Menu(string menuTitle)
        {
            AddMenuItem("Exit", OutputExit);
            this.menuTitle = menuTitle;
        }

        /// <summary>
        /// Dsiplays the menu
        /// iterates through the dictionary and prints the number folloed by the display text
        /// </summary>
        private void DisplayMenu()
        {
            System.Console.Clear();

            System.Console.WriteLine(this.menuTitle);
            System.Console.WriteLine("-------------------------------------");
            
            foreach (KeyValuePair<int, MenuItem> entry in menuItems)
            {
                System.Console.WriteLine(entry.Key + ".  " + entry.Value.Display);
            }

            System.Console.WriteLine("-------------------------------------");

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
            if (option < 0 || option > this.menuOption)
            {
                System.Console.WriteLine("Invalid option. Press any Key to continue...");
                System.Console.ReadLine();
                System.Console.Clear();
                return;
            }

            // execute the associated menu item function
            this.menuItems[option].Exec();

            // workaround for cascading menus bug
            if (option != 0)
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