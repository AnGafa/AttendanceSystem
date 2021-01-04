using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu c = new Menu();
            c.execute();

        }
    }

    class MenuItem
    {
        public delegate void executingMethod();

        public string displayName;
        public executingMethod exec;


    }



    class Menu
    {
        private List<MenuItem> menuItems = new List<MenuItem>();
        
        public Menu()
        {
            MenuItem op1 = new MenuItem();
            op1.displayName = "Option 1";
            op1.exec = outputString;
            menuItems.Add(op1);


            MenuItem op2 = new MenuItem();
            op1.displayName = "Option 2";
            op2.exec = outputString2;
            menuItems.Add(op2);

        }

        public void DisplayMenu()
        {

        }



        public void outputString()
        {
            Console.WriteLine("Executing Option 1");
        }

        public void outputString2()
        {
            Console.WriteLine("Executing Option 2");
        }




        public void executeOption(output o)
        {
            Console.WriteLine("executing...");
            o();
            Console.ReadLine();
        }

        public void execute()
        {
            executeOption(outputString);
        }


    }

}
