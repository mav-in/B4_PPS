using System;
using Domain;
using Serealization;
using HttpClient;

namespace Solution
{
  public class Solution3
  {
    public const string Url = "http://127.0.0.1";

    public static void Execute()
    {
      var port = Console.ReadLine();
      var client = new Client(Url, port);

      while (!client.Ping());

      string inputSerializationObject;
      while ((inputSerializationObject = client.GetInputData()) == string.Empty);

      var inputSerializer = Serializer<Input>.CreateSerializer("Json");
      var outputSerializer = Serializer<Output>.CreateSerializer("Json");
      var input = inputSerializer.Deserialize(inputSerializationObject);
      var outputSerializationObject = outputSerializer.Serialize(new Calculations().Calc(input));
      while (!client.WriteAnswer(outputSerializationObject)){};
    }
  }
}

