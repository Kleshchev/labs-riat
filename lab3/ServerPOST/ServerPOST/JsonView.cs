using Newtonsoft.Json;

namespace ServerJson
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
  }
}
