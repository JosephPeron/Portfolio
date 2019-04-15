using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace locationserver
{
    public class Program
    {
        static void Main(string[] args)
        {

            Server myServer = new Server();
            bool windowMode = false;

            //loops through args to check for specific inputs
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-w")
                {
                    windowMode = true;
                }
                else if (args[i] == "-t")
                {
                    myServer.timeout = int.Parse(args[i + 1]);
                }
                else if(args[i] == "-d")
                {
                    myServer.debugMode = true;
                }
            }
            //output args if in debugmode
            if(myServer.debugMode)
            {
                Console.WriteLine("Window Mode = " + windowMode.ToString());
                Console.WriteLine("Timeout Period = " + myServer.timeout);
            }
            //opens in window mode/starts ui
            if (windowMode)
            {
                //run window
                Thread serverThread = new Thread(() => myServer.runServer());
                serverThread.Start();
                WindowInterface myWindow = new WindowInterface(myServer);
                Application.Run(myWindow);
            }
            //runs in the default console mode
            else
            {
                // run console
                Console.WriteLine("Starting Server!");
                myServer.runServer();
                Console.ReadLine();
            }
        }
    }
}
