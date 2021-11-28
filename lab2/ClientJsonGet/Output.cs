using System.Xml.Serialization;

namespace ClientJsonGet
{
  public class Output
  {
    public decimal sumResult { get; set; }
    public int mulResult { get; set; }
    public decimal[] sortedInputs { get; set; }
    public Output()
    {
      mulResult = 1;
      sumResult = 0;
      sortedInputs = new decimal[0];
    }
    public static bool equalsOutputs(Output first, Output second)
    {
      if (first.sumResult != second.sumResult) return false;
      if (first.mulResult != second.mulResult) return false;
      if (first.sortedInputs.Length != second.sortedInputs.Length) return false;
      for (int i = 0; i < first.sortedInputs.Length; i++)
      {
        if (first.sortedInputs[i] != second.sortedInputs[i]) return false;
      }
      return true;
    }
  }
}
