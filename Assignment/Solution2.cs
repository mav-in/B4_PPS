using System;
using Http_Server;

namespace Solution
{
  public class Solution2

{
    private const string Url = "http://127.0.0.1";

    public static void Execute()
    {
      var port = Console.ReadLine();
      new Server(string.Format("{0}:{1}/", Url, port),true);
    }
  }
}