namespace ClientJsonGet
{
  public class Input
  {
    public int K { get; set; }
    public decimal[] Sums { get; set; }
    public int[] Muls { get; set; }
    public Input(int k, decimal[] sums, int[] muls)
    {
      this.K = k;
      this.Sums = sums;
      this.Muls = muls;
    }
    public Input()
    {
      this.K = 1;
      this.Sums = new decimal[0];
      this.Muls = new int[0];
    }
  }
}
