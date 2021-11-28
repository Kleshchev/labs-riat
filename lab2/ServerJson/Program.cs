using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace ServerJson
{
    class Program
    {
        const string inputJSON = "{\"K\":10,\"Sums\":[1.01,2.02],\"Muls\":[1,4]}";

        static async Task Main()
        {
            JsonView correctJson = new JsonView(inputJSON);
            correctJson.calculateOutput();
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://127.0.0.1:8888/");
            listener.Start();
            Console.WriteLine("Ожидание подключений...");
            int responseCount = 0;
            while (true)
            {
                HttpListenerContext context = await listener.GetContextAsync();
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;


                if (request.RawUrl.ToLower().Contains("/favicon.ico")) continue;
                string responseString = "error";
                if (request.RawUrl.ToLower().Contains("/ping"))
                {
                    response.StatusCode = (int)HttpStatusCode.OK;
                    responseString = $"{HttpStatusCode.OK}";
                }
                if (request.RawUrl.ToLower().Contains("/getinputdata"))
                {
                    responseString = inputJSON;
                }
                if (request.RawUrl.ToLower().Contains("/writeanswer"))
                {
                    string answerString = request.QueryString.Get("answer");
                    JsonView json = new JsonView(inputJSON);
                    json.calculateOutput();
                    var o1 = json.output;
                    var o2 = correctJson.output;
                    if (Output.equalsOutputs(o1, o2))
                        responseString = "Answer is right";
                    else responseString = "Incorrect answer";
                }


                Console.WriteLine(responseString);
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                response.ContentLength64 = buffer.Length;
                Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();
                responseCount++;
                if (responseCount > 5) break;
            }
            listener.Stop();
            Console.WriteLine("Обработка подключений завершена");
            Console.Read();
        }
    }
}
