using System;
using System.Net.Sockets;
using System.IO;
using System.Windows.Forms;

namespace location
{
    class Launcher
    {
        static void Main(string[] args)
        {
            Logic myLogic = new Logic();
            if (args.Length > 0)
            {
                //opens the with console if args
                ConsoleInterface myConsole = new ConsoleInterface();
                myConsole.RunConsole(args, myLogic);
                Console.ReadLine();
            }
            else
            {
                //opens the with window if no args
                Window myWindow = new Window(myLogic);
                Application.Run(myWindow);
            }
        }
    }
}


