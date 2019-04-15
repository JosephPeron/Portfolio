using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;

namespace Logic
{
    public class LogicCaluclator
    {
        public static string fromHere { get; set; } = "null";
        public static TcpClient client = new TcpClient();
        public static bool debugMode { get; set; } = false;
        public static int timeoutPeriod { get; set; } = 1000;
        public static string serverAddress { get; set; } = "localhost";
        public static int portNumber { get; set; } = 43;
        public static string lookup { get; set; } = "";
        public static string location { get; set; } = "";
        public static bool locationSet { get; set; } = false;
        public static bool lookupSet { get; set; } = false;
        public static string protocolRequest { get; set; } = "whois";

        public static void clientInitialiser(string[] args)
        {
            try
            {
                client.ReceiveTimeout = timeoutPeriod;
                client.SendTimeout = timeoutPeriod;
                client.Connect(serverAddress, portNumber);
                StreamWriter sw = new StreamWriter(client.GetStream());
                StreamReader sr = new StreamReader(client.GetStream());
                ResponseSender(args, sw, sr);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static void ResponseSender(string[] args, StreamWriter sw, StreamReader sr)
        {
            if (args.Length > 0)
            {
                switch (protocolRequest)
                {
                    case "whois":
                        WhoisResponse(lookup, location, locationSet, sw, sr);
                        if (debugMode)
                        {
                        Console.WriteLine("whois response");
                        }
                        break;
                    case "h0":
                        H0Response(lookup, location, locationSet, sw, sr);
                        if (debugMode)
                        {
                            Console.WriteLine("h0 response");
                        }
                        break;
                    case "h1":
                        H1Response(lookup, location, locationSet, sw, sr, serverAddress);
                        if (debugMode)
                        {
                            Console.WriteLine("h1 response");
                        }
                        break;
                    case "h9":
                        H9Response(lookup, location, locationSet, sw, sr);
                        if (debugMode)
                        {
                            Console.WriteLine("h9 response");
                        }
                        break;
                }
            }
            else
            {
                Console.WriteLine("Not enough arguments given");
            }
        }

        public static void WhoisResponse(string lookup, string location, bool locationSet, StreamWriter sw, StreamReader sr)
        {
            string response;
            if (!locationSet)
            {
                sw.Write(lookup + "\r\n");
                sw.Flush();
                response = sr.ReadToEnd();
                if (response == "ERROR: no entries found\r\n")
                {
                    if (fromHere == "Console")
                    {
                        printToConsole(response);
                    }
                    else if (fromHere == "UI")
                    {

                    }
                }
                else
                {
                    printToConsole(lookup + " is " + response);
                }
                    if (debugMode)
                    {

                        if (fromHere == "Console")
                        {
                            printToConsole("lookup: " + lookup);
                        }
                        else if (fromHere == "UI")
                        {

                        }
                    }

                }
            else
            {
                sw.Write(lookup + " " + location + "\r\n");
                sw.Flush();
                response = sr.ReadToEnd();
                if (response == "OK\r\n")
                {
                    if (fromHere == "Console")
                    {
                        printToConsole(lookup + " location changed to be " + location);
                    }
                }
                else if (fromHere == "UI")
                {

                }
                else
                {
                    if (fromHere == "Console")
                    {
                        printToConsole("location change failed!");
                    }
                    else if (fromHere == "UI")
                    {

                    }
                    if (debugMode)
                    {
                        if (fromHere == "Console")
                        {
                            printToConsole("lookup: " + lookup);
                            printToConsole("location: " + location);
                        }
                        else if (fromHere == "UI")
                        {

                        }

                    }
                }
            }
        }
      

        private static void H9Response(string lookup, string location, bool locationSet, StreamWriter sw, StreamReader sr)
        {
            string response;
            if (!locationSet)
            {
                sw.Write("GET /" + lookup + "\r\n");
                sw.Flush();
                response = sr.ReadToEnd();
                string[] reply = response.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                if (reply[0].Contains("404"))
                {
                    if (fromHere == "Console")
                    {
                        printToConsole("ERROR: no entries found\r\n");
                    }
                    else if (fromHere == "UI")
                    {

                    }
                    if (debugMode)
                    {
                        if (fromHere == "Console")
                        {
                            printToConsole("lookup: " + lookup);
                        }
                        else if (fromHere == "UI")
                        {

                        }
                    }
                }
                else if (reply[0].Contains("200"))
                {
                    if (fromHere == "Console")
                    {
                        Console.WriteLine(lookup + " is " + reply[3]);
                    }
                    else if (fromHere == "UI")
                    {

                    }
                }
                else
                {
                    if (fromHere == "Console")
                    {
                        printToConsole("Unexpected Error Occured");
                    }
                    else if (fromHere == "UI")
                    {

                    }
                }
            }
            else
            {
                string toSend = "PUT /" + lookup + "\r\n" + "\r\n" + location;
                sw.WriteLine(toSend);
                sw.Flush();
                response = sr.ReadToEnd();
                string[] reply = response.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                if (reply[0].Contains("OK"))
                {
                    if (fromHere == "Console")
                    {
                        printToConsole(lookup + " location changed to be " + location);
                    }
                    else if (fromHere == "UI")
                    {

                    }
                }
                else
                {
                    if (fromHere == "Console")
                    {
                        printToConsole("location change failed!");
                    }
                    else if (fromHere == "UI")
                    {

                    }
                    if (debugMode)
                    {
                        if (fromHere == "Console")
                        {
                            printToConsole("lookup: " + lookup);
                            printToConsole("location: " + location);
                        }
                        else if (fromHere == "UI")
                        {

                        }
                    }
                }
            }
        }

        private static void H0Response(string lookup, string location, bool locationSet, StreamWriter sw, StreamReader sr)
        {
            string response;
            if (!locationSet)
            {
                sw.WriteLine("GET /?" + lookup + " HTTP/1.0\r\n");
                sw.Flush();
                response = sr.ReadToEnd();
                string[] reply = response.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                if (reply[0].Contains("404"))
                {
                    if (fromHere == "Console")
                    {
                        Console.WriteLine("ERROR: no entries found\r\n");
                    }
                    else if (fromHere == "UI")
                    {

                    }
                        if (debugMode) {
                        if (fromHere == "Console")
                        {
                            printToConsole("lookup: " + lookup);
                        }
                        else if(fromHere == "UI")
                        {

                        }
                    }
                }
                else if (reply[0].Contains("200"))
                {
                    if (fromHere == "Console")
                    {
                        printToConsole(lookup + " is " + reply[3]);
                    }
                    else if (fromHere == "UI")
                    {

                    }
                }
                else
                {
                    if (fromHere == "Console")
                    {
                        printToConsole("Unexpected Error Occured");
                    }
                    else if(fromHere == "UI")
                    {

                    }
                }
            }
            else
            {
                string toSend = "POST /" + lookup + " HTTP/1.0\r\n" + "Content-Length: " + location.Length + "\r\n\r\n" + location;
                sw.WriteLine(toSend);
                sw.Flush();
                response = sr.ReadToEnd();
                string[] reply = response.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                if (reply[0].Contains("OK"))
                {
                    if (fromHere == "Console")
                    {
                        printToConsole(lookup + " location changed to be " + location);
                    }
                    else if(fromHere == "UI")
                    {

                    }
                }
                else
                {
                    if (fromHere == "Console")
                    {
                        printToConsole("location change failed!");
                    }
                    else if (fromHere == "UI")
                    {

                    }
                    if (debugMode)
                    {
                        if (fromHere == "Console")
                        {
                            printToConsole("lookup: " + lookup);
                            printToConsole("location: " + location);
                        }
                        else if(fromHere == "UI")
                        {

                        }
                    }
                }
            }
        }

        private static void H1Response(string lookup, string location, bool locationSet, StreamWriter sw, StreamReader sr, string serverAddress)
        {
            string response;
            if (!locationSet)
            {
                sw.WriteLine("GET /?name=" + lookup + " HTTP/1.1\r\nHost: " + serverAddress);
                sw.Flush();
                response = sr.ReadToEnd();
                string[] reply = response.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                if (reply[0].Contains("404"))
                {
                    if (fromHere == "Console")
                    {
                        printToConsole("ERROR: no entries found\r\n");
                    }
                    else if (fromHere == "UI")
                    {

                    }
                    if (debugMode)
                    {
                        if (fromHere == "Console")
                        {
                            printToConsole("lookup: " + lookup);
                        }
                        else if(fromHere == "UI")
                        {

                        }
                    }
                }
                else if (reply[0].Contains("200"))
                {
                    if (fromHere == "Console")
                    {
                         printToConsole(lookup + " is " + reply[3]);
                    }
                    else if (fromHere == "UI")
                    {

                    }
                }
                else
                {
                    if (fromHere == "Console")
                    {
                        printToConsole("Unexpected Error Occured");
                    }
                    else if (fromHere == "UI")
                    {

                    }
                }
            }
            else
            {
                int contentLength = 15 + lookup.Length + location.Length;
                string toSend = "POST / HTTP/1.1\r\nHost: " + serverAddress + "\r\nContent-Length: " + contentLength + "\r\n\r\nname=" + lookup + "&location=" + location;
                sw.WriteLine(toSend);
                sw.Flush();
                response = sr.ReadToEnd();
                string[] reply = response.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                if (reply[0].Contains("OK"))
                {
                    if (fromHere == "Console")
                    {
                        printToConsole(lookup + " location changed to be " + location);
                    }
                    else if (fromHere == "UI")
                    {

                    }
                }
                else
                {
                    if (fromHere == "Console")
                    {
                        printToConsole("location change failed!");
                    }
                    else if (fromHere == "UI")
                    {

                    }
                    if (debugMode)
                    {
                        if (fromHere == "Console")
                        {
                            printToConsole("lookup: " + lookup);
                            printToConsole("location: " + location);
                        }
                        else if (fromHere == "UI")
                        {

                        }
                    }
                }
            }

        }

        public static void printToConsole(string message)
        {
            Console.WriteLine(message);
        }
    }
}
