using System.Linq;

namespace ClientJsonGet
{
  abstract class Calculator
  {
    public Input input;
    public Output output;
    public abstract string outputText { get; set; }
    public Calculator(string input)
    {
      this.input = getInput(input);
    }
    public void calculateOutput()
    {
      decimal sumResult = 0;
      int mulResult = 1;
      foreach(var num in input.Sums)
      {
        sumResult += num;
      }
      sumResult *= input.K;
      foreach(var num in input.Muls)
      {
        mulResult *= num;
      }
      var sortedInputs = input.Sums.
        Concat(input.Muls.Select(x => (decimal)x)).
        OrderBy(x => x).
        ToArray();
      output = new Output();
      output.mulResult = mulResult;
      output.sortedInputs = sortedInputs;
      output.sumResult = sumResult;
      outputToText();
    }

    public abstract Input getInput(string inputText);
    public abstract void outputToText();
  }
}
