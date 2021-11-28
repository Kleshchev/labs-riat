using System;

namespace xmljson1
{
  class Program
  {
    static void Main(string[] args)
    {
      Calculator calculator;
      string type = Console.ReadLine();
      if (type.ToLower().Contains("json"))
      {
        calculator = new JsonView(Console.ReadLine());
        calculator.calculateOutput();
        Console.WriteLine(calculator.outputText);
      }
      else if (type.ToLower().Contains("xml"))
      {
        calculator = new XmlViev(Console.ReadLine());
        calculator.calculateOutput();
        Console.WriteLine(calculator.outputText);
      }
      Console.ReadLine();
    }
  }
}
