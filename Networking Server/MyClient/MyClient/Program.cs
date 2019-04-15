//Demonstrate Sockets
using System;
using System.Net.Sockets;
using System.IO;
public class Whois
{
    static void Main(string[] args)
    {
        //int c;
        if (args.Length == 0)
        {
            Console.WriteLine("No args given!");
        }
        else
        {
            try
            {
                TcpClient client = new TcpClient();
                client.Connect("whois.net.dcs.hull.ac.uk", 43);
                StreamWriter sw = new StreamWriter(client.GetStream());
                StreamReader sr = new StreamReader(client.GetStream());
                string response;
                if (args.Length == 1)
                {
                    sw.Write(args[0] + "\r\n");
                    sw.Flush();
                    response = sr.ReadToEnd();
                    Console.WriteLine(args[0] + " is " + response);
                }
                else if (args.Length == 2)
                {
                    sw.Write(args[0] + " " + args[1] + "\r\n");
                    sw.Flush();
                    response = sr.ReadToEnd();
                    if (response == "OK\r\n")
                    {
                        Console.WriteLine(args[0] + " location changed to be " + args[1]);
                    }
                } 
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}