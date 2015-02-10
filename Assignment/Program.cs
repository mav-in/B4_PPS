using System;

namespace Solution
{
  class Program
  {
    static void Main(string[] args)
    {
        if (args == null) throw new ArgumentNullException("args");
        Solution3.Execute();
    }
  }
}
