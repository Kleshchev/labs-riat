using System.Xml.Serialization;

namespace xmljson1
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
  }
}
