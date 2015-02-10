using Newtonsoft.Json;

namespace Serealization
{
  // Json-сериализатор
  public class JsonSerialization<T> : ISerializer<T>
  {
    public string Serialize(T request)
    {
        return JsonConvert.SerializeObject(request);
    }

    public T Deserialize(string request)
    {
        return JsonConvert.DeserializeObject<T>(request);
    }
  }
}
