using System.Xml.Serialization;

namespace ServerJson
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
      if (first.mulResult != second.mulResult) return false;
      if (first.sumResult != second.sumResult) return false;
      if (first.sortedInputs.Length != second.sortedInputs.Length) return false;
      var s1 = first.sortedInputs;
      var s2 = second.sortedInputs;
      for (int i = 0; i < s1.Length; i++)
        if (s1[i] != s2[i]) return false;
      return true;
    }
  }
}
