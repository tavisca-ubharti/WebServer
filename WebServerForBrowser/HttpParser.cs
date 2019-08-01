namespace WebServerForBrowser
{
    public static class HttpParser
    {
        public static string[] ParseRequest(string requestData)
        {
            var requestLine = requestData.Split("\n");
            var requestParameter = requestLine[0].Split(' ');
            return requestParameter;
        }
    }
}
