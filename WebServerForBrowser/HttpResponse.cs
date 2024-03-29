﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace WebServerForBrowser
{
    public static class HttpResponse
    {
        public static void SendResponse(Socket clientHandler, string httpMethod, string requestPath)
        {
            byte[] file = null;
            if (requestPath.Equals("/"))
                requestPath = "www" + requestPath + "index.html";
            else
                requestPath = "www" + requestPath;
            try
            {
                file = File.ReadAllBytes(requestPath);
            }
            catch
            {
                requestPath = "www" + "/error.html";
                file = File.ReadAllBytes(requestPath);
            }

            StringBuilder sbHeader = new StringBuilder();

            sbHeader.AppendLine("HTTP/1.1 200 OK");

            var pathFormat = requestPath.Split('.');
            var fileFormat = pathFormat[pathFormat.Length - 1].Trim();
            if(!fileFormat.Equals("html"))
                sbHeader.AppendLine("Content-Type: image/"+fileFormat+";charset=UTF-8");
            else
                sbHeader.AppendLine("Content-Type: text/"+fileFormat+";charset=UTF-8");
            sbHeader.AppendLine();

            List<byte> response = new List<byte>();

            response.AddRange(Encoding.ASCII.GetBytes(sbHeader.ToString()));

            response.AddRange(file);

            byte[] responseByte = response.ToArray();
            Console.WriteLine("hello");
            clientHandler.Send(responseByte);

        }
    }
}
