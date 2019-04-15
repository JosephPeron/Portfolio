using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace location
{
    class ConsoleInterface
    {
        Logic _logic;

        //methods which runs all the functionality for console request and read
        public void RunConsole(string[] args, Logic myLogic)
        {
            _logic = myLogic;
            ArgReader(args);
            string response = _logic.clientInitialiser();
            printToConsole(response);
        }

        //loops through the args and sets the relavant values if present 
        private void ArgReader(string[] args)
        {
            for (int i = 0; i < args.Length; i += 1)
            {
                switch (args[i])
                {
                    case "-h":
                        _logic.serverAddress = args[i + 1];
                        i += 1;
                        break;
                    case "-p":
                        _logic.portNumber = int.Parse(args[i + 1]);
                        i += 1;
                        break;
                    case "-t":
                        _logic.timeoutPeriod = int.Parse(args[i + 1]);
                        i += 1;
                        break;
                    case "-h0":
                        _logic.protocolRequest = "h0";
                        break;
                    case "-h1":
                        _logic.protocolRequest = "h1";
                        break;
                    case "-h9":
                        _logic.protocolRequest = "h9";
                        break;
                    case "-d":
                        _logic.debugMode = true;
                        break;
                    default:
                        if (_logic.lookupSet == false)
                        {
                            _logic.lookup = args[i];
                            _logic.lookupSet = true;
                        }
                        else if (_logic.locationSet == true)
                        {
                            string locationaddition = args[i];
                            _logic.location = _logic.location + " " + locationaddition;
                        }
                        else
                        {
                            _logic.location = args[i];
                            _logic.locationSet = true;
                        }
                        break;
                }
            }
            _logic.fromHere = "Console";
        }

        //prints message to console
        private void printToConsole(string message)
        {
            Console.WriteLine(message);
        }
    }
}
