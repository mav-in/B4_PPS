using System;
using Newtonsoft.Json;

namespace Domain
{
  [Serializable]
  [JsonObject("Input")]
  public class Input
  {
    [JsonProperty("K")]
    public int K { get; set; }

    [JsonProperty("Sums")]
    public decimal[] Sums { get; set; }

    [JsonProperty("Muls")]
    public int[] Muls { get; set; }
  }
}
