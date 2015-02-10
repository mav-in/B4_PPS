using System;
using Domain;
using Serealization;

namespace Solution
{
  public class Solution1
  {
    public static void Execute()
    {
      var readLine = Console.ReadLine();
      if (readLine != null)
      {
        var type = readLine.ToLower();
        var body = Console.ReadLine();
        var inputSerializer = Serializer<Input>.CreateSerializer(type);
        var outputSerializer = Serializer<Output>.CreateSerializer(type);
        var input = inputSerializer.Deserialize(body);
        Console.WriteLine(outputSerializer.Serialize(new Calculations().Calc(input)));
      }
    }
  }
}