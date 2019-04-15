using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using ConsoleInterface;
using WindowsInterface;

namespace Launcher
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                ConsoleWindow.RunConsole(args);
                Console.ReadLine();
            }
            else
            {
                MainWindow myWindow = new MainWindow();
                Application.Run(myWindow);
            }
        }
    }
}
