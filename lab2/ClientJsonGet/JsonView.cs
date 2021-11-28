using Newtonsoft.Json;

namespace ClientJsonGet
{
  class JsonView : Calculator
  {
    public override string outputText { get; set; }
    public JsonView(string input) : base(input) { }
    public override Input getInput(string inputText)
    {
      return JsonConvert.DeserializeObject<Input>(inputText);
    }

    public override void outputToText()
    {
      outputText = JsonConvert.SerializeObject(output);
    }
    public Output outPutfromString(string jsonText)
    {
      return JsonConvert.DeserializeObject<Output>(jsonText);
    }
  }
}
