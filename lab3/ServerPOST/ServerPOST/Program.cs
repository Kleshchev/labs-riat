using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Web;
namespace ServerPOST
{
    internal class Program
    {
        static async Task Main()
        {
            ServerJson.JsonView json;
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://127.0.0.1:1234/");
            listener.Start();
            Console.WriteLine("Ожидание подключений...");
            int responseCount = 0;
            bool isOver = false;
            while (!isOver)
            {
                HttpListenerContext context = await listener.GetContextAsync();
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;


                if (request.RawUrl.ToLower().Contains("/favicon.ico") || request.HttpMethod != "POST") continue;
                string responseString = "error";
                if (request.RawUrl.ToLower().Contains("/ping"))
                {
                    response.StatusCode = (int)HttpStatusCode.OK;
                    responseString = $"{HttpStatusCode.OK}";
                }
                else  if (request.RawUrl.ToLower().Contains("/postinputdata"))
                {
                    using (StreamReader sr = new StreamReader(request.InputStream, request.ContentEncoding))
                    {
                        string line = sr.ReadToEnd();
                        Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
                        var dataArray = line.Split('&');
                        foreach (var keyValue in dataArray)
                        {
                            var pair = keyValue.Split('=');
                            keyValuePairs.Add(pair[0], HttpUtility.UrlDecode(pair[1]));
                        }
                        json = new ServerJson.JsonView(keyValuePairs["input"]);
                        json.calculateOutput();
                        responseString = json.outputText;
                    }
                }
                else if (request.RawUrl.ToLower().Contains("/stop"))
                {
                    responseString = "Server is closed";
                    isOver = true;
                }
                Console.WriteLine($"({responseCount}) : {responseString}");

                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                response.ContentLength64 = buffer.Length;
                Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();
                responseCount++;
                if (responseCount > 5) isOver = true;
            }
            listener.Stop();
            Console.WriteLine("Обработка подключений завершена");
        }
    }
}
