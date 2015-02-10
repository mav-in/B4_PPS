using System;
using Newtonsoft.Json;

namespace Domain
{
  [Serializable]
  [JsonObject("Output")]
  public class Output
  {
    [JsonProperty("SumResult")]
    public decimal SumResult { get; set; }

    [JsonProperty("MulResult")]
    public int MulResult { get; set; }

    [JsonProperty("SortedInputs")]
    public decimal[] SortedInputs { get; set; }

    //[System.Xml.Serialization.XmlIgnoreAttribute]
  }
}
