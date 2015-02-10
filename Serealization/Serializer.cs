using System.Collections.Generic;

namespace Serealization
{
  // Сериализаторы
  public static class Serializer<T>
  {
    // Создаём сериализатор
    public static ISerializer<T> CreateSerializer(string serializationType)
    {
      var serialize = new Dictionary<string, ISerializer<T>>
      {
          {"json", new JsonSerialization<T>()},
          {"xml", new XmlSerialization<T>()}
      };
      return serialize[serializationType];
    }
  }
}
