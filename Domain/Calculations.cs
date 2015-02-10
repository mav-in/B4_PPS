using System.Linq;

namespace Domain
{
    public class Calculations
    {
      public Output Calc(Input input)
      {
        Output output = new Output
        {
          SumResult = input.Sums.Sum()*input.K,
          MulResult = input.Muls.Aggregate((x, p) => x*p),
          SortedInputs = input.Muls
            .Select(m => decimal.Parse(m.ToString()))
            .Union(input.Sums)
            .OrderBy(m => m)
            .ToArray()
        };
        return output;
      }
    }
}