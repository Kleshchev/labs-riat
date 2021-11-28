using System.Linq;

namespace xmljson1
{
  abstract class Calculator
  {
    protected Input _input;
    protected Output _output;
    public abstract string outputText { get; set; }
    public Calculator(string input)
    {
      _input = getInput(input);
    }
    public void calculateOutput()
    {
      decimal sumResult = 0;
      int mulResult = 1;
      foreach(var num in _input.Sums)
      {
        sumResult += num;
      }
      sumResult *= _input.K;
      foreach(var num in _input.Muls)
      {
        mulResult *= num;
      }
      var sortedInputs = _input.Sums.
        Concat(_input.Muls.Select(x => (decimal)x)).
        OrderBy(x => x).
        ToArray();
      _output = new Output();
      _output.mulResult = mulResult;
      _output.sortedInputs = sortedInputs;
      _output.sumResult = sumResult;
      outputToText();
    }

    public abstract Input getInput(string inputText);
    public abstract void outputToText();
  }
}
