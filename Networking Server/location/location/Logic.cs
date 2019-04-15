using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;

namespace location
{
    public class Logic
    {
        //variables which are used in the request
        public string fromHere { get; set; } = "null";
        public TcpClient client;
        public bool debugMode { get; set; } = false;
        public int timeoutPeriod { get; set; } = 1000;
        //public string serverAddress { get; set; } = "localhost";
        public string serverAddress { get; set; } = "whois.net.dcs.hull.ac.uk";
        public int portNumber { get; set; } = 43;
        public string lookup { get; set; } = "";
        public string location { get; set; } = "";
        public bool locationSet { get; set; } = false;
        public bool lookupSet { get; set; } = false;
        public string protocolRequest { get; set; } = "whois";

        public string clientInitialiser()
        {
            string response = "";
            try
            {
                //connects to the server
                client = new TcpClient();
                //sets timeout period and connects to the server at an address and port
                client.ReceiveTimeout = timeoutPeriod;
                client.SendTimeout = timeoutPeriod;
                client.Connect(serverAddress, portNumber);
                StreamWriter sw = new StreamWriter(client.GetStream());
                StreamReader sr = new StreamReader(client.GetStream());
                //incharge of creating the request
                response = ResponseSender(sw, sr);              
            }
            //error repsonse
            catch (System.Net.Sockets.SocketException e)
            {
                Console.WriteLine(e);
                response = "Server refused connection";
            }
            catch (TimeoutException e)
            {
                Console.WriteLine(e);
                response = "Connection timed out";                   
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                response = "Exception Occured!";
            }
            return response;
        }

        //decides which response the user requested to use
        public string ResponseSender(StreamWriter sw, StreamReader sr)
        {
            string response = "";
            switch (protocolRequest)
            {
                case "whois":
                    response = WhoisResponse(lookup, location, locationSet, sw, sr);
                    if(debugMode)
                    {
                        Console.WriteLine("whois response");
                    }
                    break;
                case "h0":
                    response = H0Response(lookup, location, locationSet, sw, sr);
                    if (debugMode)
                    {
                        Console.WriteLine("h0 response");
                    }
                    break;
                case "h1":
                    response = H1Response(lookup, location, locationSet, sw, sr, serverAddress);
                    if(debugMode)
                    {
                        Console.WriteLine("h1 response");
                    }
                    break;
                case "h9":
                    response = H9Response(lookup, location, locationSet, sw, sr);
                    if (debugMode)
                    {
                        Console.WriteLine("h9 response");
                    }
                    break;
            }
            return response;
        }

        //all the different types of protocol response
        public string WhoisResponse(string lookup, string location, bool locationSet, StreamWriter sw, StreamReader sr)
        {
            string response;
            if (!locationSet)
            {
                sw.Write(lookup + "\r\n");
                sw.Flush();
                response = sr.ReadToEnd();
                if (debugMode)
                {
                    Console.WriteLine("lookup: " + lookup);
                }
                if (response == "ERROR: no entries found\r\n")
                {
                    return response;
                }
                else
                {
                    string reply = lookup + " is " + response;
                    return reply;
                }
            }
            else
            {
                sw.Write(lookup + " " + location + "\r\n");
                sw.Flush();
                response = sr.ReadToEnd();
                if (response == "OK\r\n")
                {
                    string reply = (lookup + " location changed to be " + location);
                    return reply;
                    if(debugMode)
                    {
                        Console.WriteLine("lookup: " + lookup);
                        Console.WriteLine("location: " + location);
                    }
                }
                else
                {
                    if (debugMode)
                    {
                            Console.WriteLine("lookup: " + lookup);
                            Console.WriteLine("location: " + location);
                    }
                    string reply = ("location change failed!");
                    return reply;         
                }
            }
        }

        private string H9Response(string lookup, string location, bool locationSet, StreamWriter sw, StreamReader sr)
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
                    if (debugMode)
                    {
                            Console.WriteLine("lookup: " + lookup);
                    }
                    string stringback = ("ERROR: no entries found\r\n");
                    return stringback;

                }
                else if (reply[0].Contains("200"))
                {
                    string stringback = (lookup + " is " + reply[3]);
                    return stringback;
                }
                else
                {
                    string stringback = ("Unexpected Error Occured");
                    return stringback;
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
                    string stringback = (lookup + " location changed to be " + location);
                    return stringback;
                    if (debugMode)
                    {
                        Console.WriteLine("lookup: " + lookup);
                        Console.WriteLine("location: " + location);
                    }
                }
                else
                {
                    if (debugMode)
                    {
                        Console.WriteLine("lookup: " + lookup);
                        Console.WriteLine("location: " + location);
                    }
                    string stringback = ("location change failed!");
                    return stringback;
                }
            }
        }

        private string H0Response(string lookup, string location, bool locationSet, StreamWriter sw, StreamReader sr)
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
                    if (debugMode)
                    {
                            Console.WriteLine("lookup: " + lookup);
                    }
                    string stringback = ("ERROR: no entries found\r\n");
                    return stringback;
                }
                else if (reply[0].Contains("200"))
                {

                    string stringback = (lookup + " is " + reply[3]);
                    return stringback;

                }
                else
                {
                    string stringback = ("Unexpected Error Occured");
                    return stringback;

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

                    string stringback = (lookup + " location changed to be " + location);
                    return stringback;
                    if (debugMode)
                    {
                        Console.WriteLine("lookup: " + lookup);
                        Console.WriteLine("location: " + location);
                    }

                }
                else
                {
                    if (debugMode)
                    {
                        Console.WriteLine("lookup: " + lookup);
                        Console.WriteLine("location: " + location); 
                    }
                    string stringback = ("location change failed!");
                    return stringback;               
                }
            }
        }

        private string H1Response(string lookup, string location, bool locationSet, StreamWriter sw, StreamReader sr, string serverAddress)
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
                    if (debugMode)
                    {
                           Console.WriteLine("lookup: " + lookup);

                    }
                    string stringback = ("ERROR: no entries found\r\n");
                    return stringback;
                }
                else if (reply[0].Contains("200"))
                {
                    string stringback = (lookup + " is " + reply[3]);
                    return stringback;

                }
                else
                {
                    string stringback = ("Unexpected Error Occured");
                    return stringback;
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
                    string stringback = (lookup + " location changed to be " + location);
                    return stringback;
                    if (debugMode)
                    {
                        Console.WriteLine("lookup: " + lookup);
                        Console.WriteLine("location: " + location);
                    }
                }
                else
                {
                    if (debugMode)
                    {
                            Console.WriteLine("lookup: " + lookup);
                            Console.WriteLine("location: " + location);
                    }
                    string stringback = ("location change failed!");
                    return stringback;               
                }
            }
        }
    }
}
