using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ClientJsonGet
{
  class Program
  {
    static readonly HttpClient client = new HttpClient();
    static async Task Main()
    {
      // Call asynchronous network methods in a try/catch block to handle exceptions.
      try
      {
        HttpResponseMessage response = await client.GetAsync("http://127.0.0.1:8888/ping");
        response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        // Above three lines can be replaced with new helper method below
        // string responseBody = await client.GetStringAsync(uri);

        Console.WriteLine($"Ответ на /ping: {responseBody}");
        response = await client.GetAsync("http://127.0.0.1:8888/getinputdata");
        response.EnsureSuccessStatusCode();
        responseBody = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Ответ на /getinputdata: {responseBody}");
        JsonView json = new JsonView(responseBody);
        json.calculateOutput();
        Console.WriteLine($"Вычисленный ответ на задание: {json.outputText}");

        response = await client.GetAsync($"http://127.0.0.1:8888/writeanswer?answer=\"{json.outputText}\"");
        response.EnsureSuccessStatusCode();
        responseBody = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Оценка ответа: {responseBody}");
      }
      catch (HttpRequestException e)
      {
        Console.WriteLine("\nException Caught!");
        Console.WriteLine("Message :{0} ", e.Message);
      }
    }
  }
}
