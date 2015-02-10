namespace Serealization
{
  // Интерфейс сериализатора
  public interface ISerializer<T>
  {
    // Сериализовать объект
    string Serialize(T request);

    // Десериализовать объект
    T Deserialize(string request);
  }
}
