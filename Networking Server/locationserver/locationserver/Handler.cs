using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace locationserver
{
    public class Handler
    {
        Data myData = new Data();
        public void doRequest(Socket connection, int timeoutPeriod, bool debugMode)
{
            NetworkStream socketStream;
            socketStream = new NetworkStream(connection);
            if (debugMode)
            {
                Console.WriteLine("Connection Detected!");
            }
            try
            {
                //sets timeout period
                socketStream.ReadTimeout = timeoutPeriod;
                socketStream.WriteTimeout = timeoutPeriod;
                if (debugMode)
                { 
                    Console.WriteLine("doRequest entered");
                }
                StreamWriter sw = new StreamWriter(socketStream);
                StreamReader sr = new StreamReader(socketStream);
                //reads the incoming data
                string request = sr.ReadLine();
                if (debugMode)
                {
                    Console.WriteLine("request received: " + request);
                }
                //decides from the data which response to give
                #region SwitchStatement
                if (request.Contains("HTTP/1.0"))
                {
                    string[] dataSent = request.Split(new string[] { "\r\n", " ", "/", "?", "=" }, StringSplitOptions.None);
                    H0Split(dataSent, sw, sr, request, debugMode);
                }
                else if (request.Contains("HTTP/1.1"))
                {
                    string[] dataSent = request.Split(new string[] { "\r\n", " ", "/", "?", "=" }, StringSplitOptions.None);
                    H1Split(dataSent, sw, sr, request, debugMode);
                }
                else if (request.Contains("GET") || request.Contains("PUT"))
                {
                    string[] dataSent = request.Split(new string[] { "\r\n", " ", "/", "?", "=" }, StringSplitOptions.None);
                    H9Split(dataSent, sw, sr, request,debugMode);
                }
                else
                {
                    string[] dataSent = request.Split(new string[] { "\r\n", " " }, StringSplitOptions.None);
                    string location = "";
                    for (int i = 1; i < dataSent.Length - 1; i++)
                    {
                        location = location + dataSent[i] + " ";
                    }
                    location = location + dataSent[dataSent.Length - 1];
                    WhoIsSplit(dataSent, sw, request, location, debugMode);
                }
                #endregion 
            }
            //error occured
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                //always close the connection to the server
                socketStream.Close();
                connection.Close();
                if (debugMode)
                {
                    Console.WriteLine("Connection Ended!");
                }
            }
        }
        //all the seperate response protocols 
        public void WhoIsSplit(string[] data, StreamWriter sw, string request, string location, bool debugMode)
        {
            string lookup = " ";
            string update = " ";

            if (data.Length < 2)
            {
                lookup = data[0];
                update = "n/a";
            }
            else
            {
                lookup = data[0];
                update = location;
            }
            WhoIsResponse(lookup, update, sw, debugMode);
        }
        public void WhoIsResponse(string lookup, string update, StreamWriter sw, bool debugMode)
        {
            if (update == "n/a")
            {
                if (myData.checkDictionary(lookup))
                {
                    string location = myData.getLocation(lookup);
                    sw.WriteLine(location);
                    if (debugMode)
                    {
                        Console.WriteLine("I sent " + lookup + "'s information: " + location);
                    }
                }
                else
                {
                    sw.WriteLine("ERROR: no entries found");
                }
            }
            else
            {
                myData.changeDictionary(lookup,update);
                if (debugMode)
                {
                    Console.WriteLine("Changed " + lookup + "'s location to " + update);
                }
                sw.WriteLine("OK");
            }
            sw.Flush();
        }

        private void H9Split(string[] data, StreamWriter sw, StreamReader sr, string request, bool debugMode)
        {
            string lookup = " ";
            string update = " ";
            if (request.Contains("GET"))
            {
                lookup = data[2];
                update = "n/a";
            }
            else
            {
                string secondLine = sr.ReadLine();
                string thirdLine = sr.ReadLine();
                lookup = data[2];
                update = thirdLine;
            }
            H9Response(lookup, update, sw, debugMode);
        }
        private void H9Response(string lookup, string update, StreamWriter sw, bool debugMode)
        {
            if (update == "n/a")
            {
                if (myData.checkDictionary(lookup))
                {
                    string location = myData.getLocation(lookup);
                    sw.WriteLine("HTTP/0.9 200 OK\r\nContent-Type: text/plain\r\n\r\n" + location);
                    if (debugMode)
                    {
                        Console.WriteLine("I sent " + lookup + "'s information: " + location);
                    }
                }
                else
                {
                    sw.WriteLine("HTTP/0.9 404 Not Found\r\nContent-Type: text/plain\r\n");
                }
            }
            else
            {
                myData.changeDictionary(lookup, update);
                if (debugMode)
                {
                    Console.WriteLine("Changed " + lookup + "'s location to " + update);
                }
                sw.WriteLine("HTTP/0.9 200 OK\r\nContent-Type: text/plain\r\n");
            }
            sw.Flush();
        }

        private void H0Split(string[] data, StreamWriter sw, StreamReader sr, string request, bool debugMode)
        {
            string lookup = " ";
            string update = " ";
            if (request.Contains("GET"))
            {
                lookup = data[3];
                update = "n/a";
            }
            else
            {
                string secondLine = sr.ReadLine();
                string thirdLine = sr.ReadLine();
                string fourthLine = sr.ReadLine();
                lookup = data[2];
                update = fourthLine;
            }
            H0Response(lookup, update, sw, debugMode);
        }
        private void H0Response(string lookup, string update, StreamWriter sw, bool debugMode)
        {
            if (update == "n/a")
            {
                if (myData.checkDictionary(lookup))
                {
                    string location = myData.getLocation(lookup);
                    sw.WriteLine("HTTP/1.0 200 OK\r\nContent-Type: text/plain\r\n\r\n" + location);
                    if (debugMode)
                    {
                        Console.WriteLine("I sent " + lookup + "'s information: " + location);
                    }
                }
                else
                {
                    sw.WriteLine("HTTP/1.0 404 Not Found\r\nContent-Type: text/plain\r\n");
                }
            }
            else
            {
                myData.changeDictionary(lookup, update);
                if (debugMode)
                {
                    Console.WriteLine("Changed " + lookup + "'s location to " + update);
                }
                sw.WriteLine("HTTP/1.0 200 OK\r\nContent-Type: text/plain\r\n");
            }
            sw.Flush();
        }

        private void H1Split(string[] data, StreamWriter sw, StreamReader sr, string request, bool debugMode)
        {
            string lookup = " ";
            string update = " ";
            if (request.Contains("GET"))
            {
                lookup = data[4];
                update = "n/a";
            }
            else
            {
                string secondLine = sr.ReadLine();
                string thirdLine = sr.ReadLine();
                string fourthLine = sr.ReadLine();
                string fifthLine = sr.ReadLine();
                string[] moreData = fifthLine.Split(new string[] { "=", "&" }, StringSplitOptions.None);
                lookup = moreData[1];
                update = moreData[3];
            }
            H1Response(lookup, update, sw, debugMode);
        }
        private void H1Response(string lookup, string update, StreamWriter sw, bool debugMode)
        {
            if (update == "n/a")
            {
                if (myData.checkDictionary(lookup))
                {
                    string location = myData.getLocation(lookup);
                    sw.WriteLine("HTTP/1.1 200 OK\r\nContent-Type: text/plain\r\n\r\n" + location);
                    if (debugMode)
                    {
                        Console.WriteLine("I sent " + lookup + "'s information: " + location);
                    }
                }
                else
                {
                    sw.WriteLine("HTTP/1.1 404 Not Found\r\nContent-Type: text/plain\r\n");
                }
            }
            else
            {
                myData.changeDictionary(lookup, update);
                if (debugMode)
                {
                    Console.WriteLine("Changed " + lookup + "'s location to " + update);
                }
                sw.WriteLine("HTTP/1.1 200 OK\r\nContent-Type: text/plain\r\n");
            }
            sw.Flush();
        }
    }
}
