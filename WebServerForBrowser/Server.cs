using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace WebServerForBrowser
{
    public static class Server
    {
        public static void StartServer(int portNumber)
        {
            IPHostEntry iPHostEntry = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress iPAddress = iPHostEntry.AddressList[1];
            Console.WriteLine($"Server Ip Address : {iPAddress}:{portNumber}");
            IPEndPoint localEndPoint = new IPEndPoint(iPAddress, portNumber);
            Socket connectionListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                connectionListener.Bind(localEndPoint);
                connectionListener.Listen(100);
                Console.WriteLine("Waiting connection ... ");
                while (true)
                {
                    var clientHandler = connectionListener.Accept();
                    var handleRequestThread = new Thread(new ThreadStart(() => HttpRequest.HandleRequest(clientHandler)));
                    handleRequestThread.Start();
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.ToString());
            }
        }
        
    }
}
