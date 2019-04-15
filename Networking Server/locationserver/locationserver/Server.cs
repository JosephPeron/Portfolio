using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;

namespace locationserver
{
    public class Server
    {
        //variable declaration 
        public bool listening { get; set; } = true;
        public int timeout { get; set; } = 1000;
        public bool debugMode { get; set; } = false;

        //method which runs the server 
        public void runServer()
        {
            Handler myHandler = new Handler();
            TcpListener listener;
            Socket connection;
            try
            {
                listener = new TcpListener(IPAddress.Any, 43);
                //loops constantly so the server is always open 
                while (true)
                {
                    listener.Start();
                    Console.WriteLine("Listener Started");
                    if (debugMode)
                    {
                        Console.WriteLine(listening.ToString());
                    }
                    // listens for incoming requests else it stops listening until re-enabled
                    while (listening)
                    {
                        connection = listener.AcceptSocket();
                        Thread t = new Thread(() => myHandler.doRequest(connection, timeout, debugMode));
                        t.Start();
                    }

                    listener.Stop();
                }
            }
            //error occured at somepoint 
             catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

        public void startServer()
        {
            listening = true;
        }

        public void stopServer()
        {
            listening = false;
        }

        public void changeTimeout(int value)
        {
            timeout = value;
        }
    }
}
