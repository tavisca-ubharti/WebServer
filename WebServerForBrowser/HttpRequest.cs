using System;
using System.Net.Sockets;
using System.Text;

namespace WebServerForBrowser
{
    public static class HttpRequest
    {
        public static void HandleRequest(Socket clientHandler)
        {
            byte[] requestHeader = new byte[1024];
            clientHandler.Receive(requestHeader);
            string requestData = Encoding.ASCII.GetString(requestHeader, 0,requestHeader.Length);
            try
            {
                var requestParameter = HttpParser.ParseRequest(requestData);
                var httpMethod = requestParameter[0];
                var requestPath = requestParameter[1];
                HttpResponse.SendResponse(clientHandler, httpMethod, requestPath);
                clientHandler.Shutdown(SocketShutdown.Both);
                clientHandler.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
