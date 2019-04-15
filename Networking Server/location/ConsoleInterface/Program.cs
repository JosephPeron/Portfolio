using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using Logic;

namespace ConsoleInterface
{
    public class ConsoleWindow
    {
        static void Main(string[] args)
        {

        }

        public static void RunConsole(string[] args)
        {
            ArgReader(args);
            LogicCaluclator.clientInitialiser(args);            
        }

        private static void ArgReader(string[] args)
        {

            for (int i = 0; i < args.Length; i += 1)
            {
                switch (args[i])
                {
                    case "-h":
                        LogicCaluclator.serverAddress = args[i + 1];
                        i += 1;
                        break;
                    case "-p":
                        LogicCaluclator.portNumber = int.Parse(args[i + 1]);
                        i += 1;
                        break;
                    case "-t":
                        LogicCaluclator.timeoutPeriod = int.Parse(args[i + 1]);
                        i += 1;
                        break;
                    case "-h0":
                        LogicCaluclator.protocolRequest = "h0";
                        break;
                    case "-h1":
                        LogicCaluclator.protocolRequest = "h1";
                        break;
                    case "-h9":
                        LogicCaluclator.protocolRequest = "h9";
                        break;
                    case "-d":
                        LogicCaluclator.debugMode = true;
                        break;
                    default:
                        if (LogicCaluclator.lookupSet == false)
                        {
                            LogicCaluclator.lookup = args[i];
                            LogicCaluclator.lookupSet = true;
                        }
                        else if (LogicCaluclator.locationSet == true)
                        {
                            string locationaddition = args[i];
                            LogicCaluclator.location = LogicCaluclator.location + " " + locationaddition;
                        }
                        else
                        {
                            LogicCaluclator.location = args[i];
                            LogicCaluclator.locationSet = true;
                        }
                        break;
                }
            }
            LogicCaluclator.fromHere = "Console";
        }
    }
}
